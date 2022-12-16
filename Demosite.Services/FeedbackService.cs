using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace Demosite.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ILogger<FeedbackService> _logger;
        private string nameService => nameof(EmailNotificationService);
        private readonly FeedbackSettings _feedbackSettings;
        private readonly EmailSenderSettings _senderSettings;
        private readonly EmailNotificationSettings _emailSettings;
        private readonly INotificationTemplateEngine _notificationTemplateEngine;
        public const string FEEDBACK_EMAIL = "Demosite.Services.EmailTemplates.FeedbackEmail.cshtml";
        public FeedbackService(IConfiguration configuration,
                               ILogger<FeedbackService> logger,
                               EmailNotificationSettings settings,
                               INotificationTemplateEngine templateEngine)
        {
            _feedbackSettings = configuration.GetSection(nameof(FeedbackSettings)).Get<FeedbackSettings>();
            _senderSettings = settings.EmailSender;
            _emailSettings = settings;
            _logger = logger;
            _notificationTemplateEngine = templateEngine;
        }
        public async Task<SmtpStatusCode> SendFeedback(FeedbackDto model)
        {
            SmtpStatusCode status = SmtpStatusCode.ServiceReady;
            using (SmtpClient SmtpClient = GetClient())
            {
                MailMessage message = new();
                try
                {
                    message.To.Add(new MailAddress(_feedbackSettings.DestinationEmail));
                    message.Subject = _feedbackSettings.Subject + " from " + model.Name;
                    message.From = new MailAddress(_feedbackSettings.FromEmail);
                    message.Body = await CreateBody(model);
                    message.IsBodyHtml = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error during initialize SMTP and create email with feedback: {ex.Message}", ex);
                    return SmtpStatusCode.GeneralFailure;
                }
                for (int i = 0; i < _emailSettings.NumberOfAttemptsSending; i++)
                {
                    try
                    {
                        await SmtpClient.SendMailAsync(message);
                        status = SmtpStatusCode.Ok;
                        break;
                    }
                    catch (SmtpException ex)
                    {
                        status = ex.StatusCode;
                        await Task.Delay(_emailSettings.TimeIntervalOfAttemptsSending * i);
                    }
                    catch (Exception)
                    {
                        status = SmtpStatusCode.GeneralFailure;
                    }
                }
            }
            return status;
        }
        private SmtpClient GetClient()
        {
            NetworkCredential credential = new(_senderSettings.UserName, _senderSettings.Password);
            if (!string.IsNullOrEmpty(_senderSettings.Domain))
            {
                credential.Domain = _senderSettings.Domain;
            }
            SmtpClient client = new(_senderSettings.SmtpServer, _senderSettings.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = credential,
                EnableSsl = _senderSettings.UseSsl
            };
            return client;
        }

        private async Task<string> CreateBody(FeedbackDto model)
        {
            string body = "";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FEEDBACK_EMAIL))
            {
                using StreamReader reader = new(stream);
                try
                {
                    body = await _notificationTemplateEngine.BuildMessage(FEEDBACK_EMAIL, await reader.ReadToEndAsync(), model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                }
            }
            return body;
        }
    }
}
