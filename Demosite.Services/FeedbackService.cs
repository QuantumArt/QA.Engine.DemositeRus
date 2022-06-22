using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Services.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.Services
{
    public class FeedbackService : IFeedbackService
    {
        private ILogger<FeedbackService> _logger { get; }
        private string nameService => nameof(EmailNotificationService);
        private FeedbackSettings _feedbackSettings { get; }
        private EmailSenderSettings _senderSettings { get; }
        private EmailNotificationSettings _emailSettings { get; }
        private INotificationTemplateEngine _notificationTemplateEngine { get; }
        public const string feedbackEmail = "Demosite.Services.EmailTemplates.FeedbackEmail.cshtml";
        public FeedbackService(IConfiguration configuration,
                               ILogger<FeedbackService> logger,
                               EmailNotificationSettings settings,
                               INotificationTemplateEngine templateEngine)
        {
            this._feedbackSettings = configuration.GetSection(nameof(FeedbackSettings)).Get<FeedbackSettings>();
            this._senderSettings = settings.EmailSender;
            this._emailSettings = settings;
            this._logger = logger;
            this._notificationTemplateEngine = templateEngine;
        }
        public async Task<SmtpStatusCode> SendFeedback(FeedbackDto model)
        {
            SmtpStatusCode status = SmtpStatusCode.ServiceReady;
            using (SmtpClient SmtpClient = GetClient())
            {
                var message = new MailMessage();
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
                    catch (Exception ex)
                    {
                        status = SmtpStatusCode.GeneralFailure;
                    }
                }
            }
            return status;
        }
        private SmtpClient GetClient()
        {
            var credential = new NetworkCredential(this._senderSettings.UserName, this._senderSettings.Password);
            if (!string.IsNullOrEmpty(this._senderSettings.Domain))
            {
                credential.Domain = this._senderSettings.Domain;
            }
            var client = new SmtpClient(this._senderSettings.SmtpServer, this._senderSettings.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = credential,
                EnableSsl = this._senderSettings.UseSsl
            };
            return client;
        }

        private async Task<string> CreateBody(FeedbackDto model)
        {
            string body = "";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(feedbackEmail))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    try
                    {
                        body = await _notificationTemplateEngine.BuildMessage(feedbackEmail, await reader.ReadToEndAsync(), model);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                    }
                }
            }
            return body;
        }
    }
}
