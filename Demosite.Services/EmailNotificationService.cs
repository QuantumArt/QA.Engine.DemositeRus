using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Enums;
using Demosite.Interfaces.Dto.Request;
using Demosite.Postgre.DAL.NotQP;
using Demosite.Postgre.DAL.NotQP.Models;
using Demosite.Services.Models;
using Demosite.Services.Settings;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace Demosite.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly PostgreDataContext _dbContext;
        private readonly INewsService _newsService;
        private readonly EmailNotificationSettings _settings;
        private readonly ILogger<EmailNotificationService> _logger;
        private string nameService => nameof(EmailNotificationService);
        private readonly string _logoFileName = "demo-logo.jpeg";
        private readonly INotificationTemplateEngine _notificationTemplateEngine;
        private readonly IWebHostEnvironment _environment;
        public const string CHECK_EMAIL_NAME = "Demosite.Services.EmailTemplates.CheckEmail.cshtml";
        public const string NEWS_EMAIL_NAME = "Demosite.Services.EmailTemplates.NewsEmail.cshtml";
        public EmailNotificationService(IDbContextNotQP dbContext,
                                        INewsService newsService,
                                        EmailNotificationSettings settings,
                                        ILogger<EmailNotificationService> logger,
                                        INotificationTemplateEngine notificationTemplateEngine,
                                        IWebHostEnvironment hostingEnvironment)
        {
            this._dbContext = dbContext as PostgreDataContext;
            _newsService = newsService;
            _settings = settings;
            _logger = logger;
            _notificationTemplateEngine = notificationTemplateEngine;
            _environment = hostingEnvironment;
        }
        public async Task BackgroundSendEmails()
        {
            _logger.LogInformation(nameService + " is starting");
            int? distributionId = await CreateDistribution();
            int[] distributionIdsNotCompleted = await CheckErrorDistribution();
            bool isHaveNotCompleted = distributionIdsNotCompleted.Any();
            if (distributionId.HasValue)
            {
                distributionIdsNotCompleted = distributionIdsNotCompleted.Append(distributionId.Value)
                                                                         .Distinct()
                                                                         .ToArray();
            }
            foreach (int id in distributionIdsNotCompleted)
            {
                await CreateAndSendEnvelope(new EnvelopeParameterRequest()
                {
                    DistributionId = id,
                    IncludeEnvelopeNotSending = isHaveNotCompleted
                });
            }
            _logger.LogInformation(nameService + " is end");
        }

        public async Task CheckIncompleledDistributions()
        {
            _logger.LogInformation(nameService + $": send all unsent email to subscibers");
            int[] distributionIdsNotCompleted = await CheckErrorDistribution();
            foreach (int id in distributionIdsNotCompleted)
            {
                await CreateAndSendEnvelope(new EnvelopeParameterRequest()
                {
                    DistributionId = id,
                    IncludeEnvelopeNotSending = true
                });
            }
            _logger.LogInformation(nameService + $": send all unsent email is completed");
        }

        public async Task<SubscriptionStatus> AddSubscriber(NewsSubscriber subscriber)
        {
            SubscriptionStatus result = new()
            {
                Success = true,
                Message = ""
            };
            _logger.LogInformation(nameService + $": add new email to subscibe: {subscriber.Email}");
            subscriber.Email = subscriber.Email.ToLower();
            EmailNewsSubscriber newSubscriber = new();
            if (subscriber.Email.Length == 0)
            {
                string message = "email address is required";
                _logger.LogInformation(nameService + ": " + message);
                return new SubscriptionStatus()
                {
                    Success = false,
                    TypeError = "email",
                    Message = message
                };
            }
            if (subscriber.NewsCategory.Length == 0)
            {
                string message = "news category is required";
                _logger.LogInformation(nameService + ": " + message);
                return new SubscriptionStatus()
                {
                    Success = false,
                    TypeError = "newsCategory",
                    Message = message
                };
            }
            EmailNewsSubscriber existSubscriber = await _dbContext.EmailNewsSubscribers.Where(s => s.Email == subscriber.Email)
                                                           .FirstOrDefaultAsync();
            if (existSubscriber != null && existSubscriber.IsActive)
            {
                string message = "this email address is already subscribed";
                _logger.LogInformation(nameService + ": " + message + $": {subscriber.Email}");
                return new SubscriptionStatus()
                {
                    Success = false,
                    TypeError = "email",
                    Message = message
                };
            }
            else if (existSubscriber != null && !existSubscriber.IsActive)
            {
                existSubscriber.FirstName = subscriber.FirstName;
                existSubscriber.LastName = subscriber.LastName;
                existSubscriber.Company = subscriber.Company;
                existSubscriber.NewsCategory = subscriber.NewsCategory;
                existSubscriber.ConfirmCode = Guid.NewGuid().ToString().ToLower();
                existSubscriber.ConfirmCodeSendDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                newSubscriber = existSubscriber;
            }
            else
            {
                newSubscriber = Map(subscriber);
                newSubscriber.ConfirmCode = Guid.NewGuid().ToString().ToLower();
                newSubscriber.ConfirmCodeSendDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                _ = _dbContext.EmailNewsSubscribers.Add(newSubscriber);
            }
            try
            {
                if (!(await SendCheckEmail(newSubscriber) == SmtpStatusCode.Ok))
                {
                    result.Success = false;
                    result.TypeError = "email";
                    result.Message = "Ошибка отправки тестового письма, пожалуйста попробуйте повторить позже или введите другой адресс";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(nameService + $": error during send check email: {ex.Message}", ex);
            }
            try
            {
                _ = await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<(bool isConfirm, string text)> ConfirmedSubscribe(string confirmcode)
        {
            confirmcode = confirmcode.ToLower();
            EmailNewsSubscriber subscriber = await _dbContext.EmailNewsSubscribers.FirstOrDefaultAsync(s => s.ConfirmCode == confirmcode);
            if (subscriber == null)
            {
                return (false, "We're sorry, but this subscription confirmation email is no longer valid. Please try again later.");
            }
            else if (subscriber != null && subscriber.IsActive)
            {
                return (false, "This email is already subscribed to news.");
            }
            else if (subscriber != null && subscriber.ConfirmCodeSendDate.HasValue && subscriber.ConfirmCodeSendDate.Value.Add(_settings.EmailConfirmationExpirationTime) < DateTime.Now)
            {
                return (false, "This email is already subscribed to news.");
            }
            subscriber.IsActive = true;
            _ = await _dbContext.SaveChangesAsync();
            return (true, "Your subscription has been confirmed.");
        }

        public async Task<SubscriptionStatus> UnSubscribe(string guid)
        {
            guid = guid.ToLower();
            EmailNewsSubscriber subscriber = await _dbContext.EmailNewsSubscribers.FirstOrDefaultAsync(s => s.ConfirmCode == guid);
            if (subscriber == null)
            {
                string message = " Not find subscriber for unsubscribe";
                _logger.LogInformation(nameService + message + $" Confirm Code to unsubscribe: {guid}");
                return new SubscriptionStatus()
                {
                    Success = false,
                    Message = message
                };
            }
            else if (subscriber != null && !subscriber.IsActive)
            {
                return new SubscriptionStatus()
                {
                    Success = true,
                    Message = "You will not receive any more emails with news"
                };
            }
            subscriber.IsActive = false;
            _ = await _dbContext.SaveChangesAsync();
            return new SubscriptionStatus()
            {
                Success = true,
                Message = "You will not receive any more emails with news"
            };
        }

        private async Task<int[]> CheckErrorDistribution()
        {
            int[] result = await _dbContext.Distributions.Where(d => d.Status == SendStatus.Processing || d.Status == SendStatus.Error)
                                                      .Select(d => d.Id)
                                                      .ToArrayAsync();
            return result;
        }
        private async Task CreateAndSendEnvelope(EnvelopeParameterRequest request)
        {
            await CreateEnvelopes(request.DistributionId.Value);
            await SendEmailsToSubscribers(request);
        }

        private async Task<SmtpStatusCode> SendCheckEmail(EmailNewsSubscriber subscriber)
        {
            SmtpStatusCode status = SmtpStatusCode.Ok;
            string base64ImageRepresentation = await GetConvertedLogoImage(_logoFileName);
            string body = "";
            EmailModel model = new()
            {
                Subscriber = new Subscriber(),
                NewsPosts = new NewsPostDto[0],
                BaseUrl = _settings.BaseURLNewsService + @"/subscribe/confirmedsubscribe?confirmcode=" + subscriber.ConfirmCode,
                LogoImage = base64ImageRepresentation
            };
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(CHECK_EMAIL_NAME))
            {
                using StreamReader reader = new(stream);
                try
                {
                    body = await _notificationTemplateEngine.BuildMessage(CHECK_EMAIL_NAME, await reader.ReadToEndAsync(), model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                }
            }
            Envelope envelope = new()
            {
                Body = body,
                Subscriber = subscriber,
                Status = SendStatus.Created,
                NumberOfAttempts = 0
            };
            using (SmtpClient SmtpClient = GetClient())
            {
                try
                {
                    status = (await SendEmailAsync(new[] { envelope }, SmtpClient, "Email Confirmation", false)).status;
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error during send email with check code: {ex.Message}", ex);
                }
            }

            return status;
        }

        private async Task<int?> CreateDistribution()
        {
            try
            {
                _logger.LogInformation(nameService + $": creating a list of news to send");
                //получить дату последней рассылки
                DateTime lastDateSend = DateTime.Now.Subtract(_settings.SendTimeInterval);
                //получить новые новости с даты последней расылки
                IEnumerable<NewsPostDto> newNews = _newsService.GetAllPosts(new PostRequest()
                {
                    FromDate = lastDateSend,
                });
                if (!newNews.Any())
                {
                    _logger.LogInformation(nameService + $": not found new news");
                    return null;
                }
                Distribution newDistribution = new()
                {
                    NewsIds = newNews.Select(n => n.Id).ToArray(),
                    Status = SendStatus.Created,
                    Created = DateTime.Now
                };
                _ = _dbContext.Distributions.Add(newDistribution);
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation(nameService + $": new Distribution create, ID: {newDistribution.Id}");
                return newDistribution.Id;
            }
            catch
            {
                throw;
            }
        }

        private async Task CreateEnvelopes(int distributionId)
        {
            _logger.LogInformation(nameService + $": creating a list envelope");
            Distribution distribution = await _dbContext.Distributions
                                              .FirstOrDefaultAsync(d => d.Id == distributionId);
            if (distribution == null)
            {
                _logger.LogInformation(nameService + $": not found distribution with ID: {distributionId}");
                return;
            }
            distribution.Status = SendStatus.Processing;
            _ = await _dbContext.SaveChangesAsync();
            NewsPostDto[] news = _newsService.GetPosts(distribution.NewsIds)
                                   .OrderByDescending(n => n.PostDate)
                                   .ToArray();
            if (news.Length == 0)
            {
                _logger.LogInformation(nameService + $": not found new news to send");
                return;
            }
            string base64ImageRepresentation = await GetConvertedLogoImage(_logoFileName);
            IQueryable<EmailNewsSubscriber> query = _dbContext.EmailNewsSubscribers
                        .AsNoTracking()
                        .Where(s => s.IsActive);
            //для поиска недосозданных рассылок в рамках текущего distribution
            int? lastSendIdSubscriber = await _dbContext.Envelopes.Where(e => e.DistributionId == distributionId)
                                                                 .Select(e => e.SubscriberId)
                                                                 .OrderBy(e => e)
                                                                 .LastOrDefaultAsync();
            if (lastSendIdSubscriber.HasValue)
            {
                query = query.Where(s => s.Id > lastSendIdSubscriber.Value);
            }
            query = query.OrderBy(s => s.Id);
            int subscribersLength = await query.CountAsync();
            for (int i = 0; i < subscribersLength; i += _settings.CreateRecipientsSize)
            {
                EmailNewsSubscriber[] subscribers = await query.Skip(i)
                                                    .Take(_settings.CreateRecipientsSize)
                                                    .ToArrayAsync();
                if (subscribers.Length == 0)
                {
                    _logger.LogInformation(nameService + $": not found subscribers for send news");
                    break;
                }
                List<Envelope> envelopes = new(subscribers.Length);
                foreach (EmailNewsSubscriber subscriber in subscribers)
                {
                    try
                    {
                        if (!subscriber.NewsCategory.Any(c => news.Select(n => n.Category.Id).Contains(c)))
                        {
                            continue;
                        }
                        envelopes.Add(new Envelope()
                        {
                            Status = SendStatus.Created,
                            Body = await CreateBodyEmail(news, subscriber, base64ImageRepresentation),
                            SubscriberId = subscriber.Id,
                            DistributionId = distribution.Id
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(nameService + $": error during add envelope: {ex.Message}", ex);
                        distribution.Status = SendStatus.Error;
                        _ = await _dbContext.SaveChangesAsync();
                        return;
                    }
                }
                _dbContext.Envelopes.AddRange(envelopes);
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation(nameService + $": new envelopes was saved");
            };
            distribution.Status = SendStatus.Sending;
            _ = await _dbContext.SaveChangesAsync();
        }

        private async Task SendEmailsToSubscribers(EnvelopeParameterRequest request)
        {
            _logger.LogInformation(nameService + $": start sending mail");
            ExpressionStarter<Envelope> predicate = PredicateBuilder.New<Envelope>(true);
            predicate = predicate.And(e => e.Status == SendStatus.Created);
            if (request.IncludeEnvelopeNotSending)
            {
                predicate = predicate.Or(e => e.Status == SendStatus.Error);
            }
            IQueryable<Envelope> queryEnvelopes = _dbContext.Envelopes.Where(predicate);
            if (!request.IncludeEnvelopesWithExceededSend)
            {
                queryEnvelopes = queryEnvelopes.Where(e => e.NumberOfAttempts <= _settings.NumberOfAttemptsSending);
            }
            if (request.DistributionId.HasValue)
            {
                queryEnvelopes = queryEnvelopes.Where(e => e.DistributionId == request.DistributionId.Value);
            }
            if (request.EnvelopeIds != null && request.EnvelopeIds.Length > 0)
            {
                queryEnvelopes = queryEnvelopes.Where(e => request.EnvelopeIds.Contains(e.Id));
            }
            int envelopesLength = await queryEnvelopes.CountAsync();
            queryEnvelopes = queryEnvelopes.OrderBy(e => e.Id);
            using SmtpClient SmtpClient = GetClient();
            for (int i = 0; i < envelopesLength; i += _settings.MailingPacketSize)
            {
                Envelope[] envelopes = await queryEnvelopes.Skip(i)
                                     .Take(_settings.MailingPacketSize)
                                     .Include(e => e.Subscriber)
                                     .ToArrayAsync();
                if (envelopes.Length == 0)
                {
                    break;
                }
                Distribution distribution = await _dbContext.Distributions.FirstOrDefaultAsync(d => d.Id == envelopes[0].DistributionId);
                if (distribution == null)
                {
                    _logger.LogInformation(nameService + $": distribution with id: {envelopes[0].DistributionId} not find");
                }
                if (envelopes.Length == 0)
                {
                    _logger.LogInformation(nameService + $": not new envelopes to send");
                    return;
                }
                try
                {
                    (SmtpStatusCode status, int distributionId) = await SendEmailAsync(envelopes, SmtpClient, _settings.Subject);
                    if (distribution != null)
                    {
                        distribution.Status = status == SmtpStatusCode.ServiceNotAvailable ? SendStatus.Error : SendStatus.Completed;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": an error occurred while sending emails : {ex.Message}", ex);
                    return;
                }
                finally
                {
                    SmtpClient.Dispose();
                }
                _ = await _dbContext.SaveChangesAsync();
            }
        }

        private async Task<(SmtpStatusCode status, int distributionId)> SendEmailAsync(Envelope[] envelopes, SmtpClient client, string subject, bool saveContext = true)
        {
            (SmtpStatusCode status, int distributionId) result = (SmtpStatusCode.Ok, envelopes[0].DistributionId);
            //рассылка писем

            foreach (Envelope envelope in envelopes)
            {
                MailMessage message = new();
                message.To.Add(new MailAddress(envelope.Subscriber.Email));
                message.Subject = subject;
                message.From = new MailAddress(_settings.From);
                message.Body = envelope.Body;
                message.IsBodyHtml = true;
                for (int i = 1; i <= _settings.NumberOfAttemptsSending; i++)
                {
                    try
                    {
                        await client.SendMailAsync(message);
                        envelope.Status = SendStatus.Completed;
                        envelope.StatusCodeSMTP = (int)SmtpStatusCode.Ok;
                        break;
                    }
                    catch (SmtpFailedRecipientException ex)
                    {
                        _logger.LogError(nameService + $": error during send mail: {ex.Message}", ex);
                        envelope.Status = SendStatus.Error;
                        envelope.StatusCodeSMTP = (int)ex.StatusCode;
                        envelope.NumberOfAttempts++;
                    }
                    catch (SmtpException ex)
                    {
                        envelope.Status = SendStatus.Error;
                        envelope.StatusCodeSMTP = (int)ex.StatusCode;
                        if (ex.StatusCode == SmtpStatusCode.ServiceNotAvailable)
                        {
                            _logger.LogError(nameService + $": error because SMTP service not available: {ex.Message}", ex);
                            if (i == _settings.NumberOfAttemptsSending)
                            {
                                return result = (ex.StatusCode, envelope.DistributionId);
                            }
                            await Task.Delay(_settings.TimeIntervalOfAttemptsSending * i);
                        }
                        else
                        {
                            _logger.LogError(nameService + $": error during send mail: {ex.Message}", ex);
                            envelope.NumberOfAttempts++;
                            result = (ex.StatusCode, envelope.DistributionId);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(nameService + $": internal error during send mail: {ex.Message}", ex);
                        envelope.Status = SendStatus.Error;
                        envelope.StatusCodeSMTP = null;
                        envelope.NumberOfAttempts++;
                        result = (SmtpStatusCode.GeneralFailure, envelope.DistributionId);
                    }
                }
                if (saveContext)
                {
                    _ = await _dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        private async Task<string> CreateBodyEmail(NewsPostDto[] newsArray, EmailNewsSubscriber subscriber, string logoImageBASE64)
        {
            string body = "";
            EmailModel model = new()
            {
                Subscriber = new Subscriber()
                {
                    ConfirmCode = subscriber.ConfirmCode,
                    FirstName = subscriber.FirstName,
                    LastName = subscriber.LastName,
                    Company = subscriber.Company
                },
                NewsPosts = newsArray.Where(n => subscriber.NewsCategory.Contains(n.Category.Id))
                                     .ToArray(),
                BaseUrl = _settings.BaseURLNewsService,
                LogoImage = logoImageBASE64
            };
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(NEWS_EMAIL_NAME))
            {
                using StreamReader reader = new(stream);
                try
                {
                    body = await _notificationTemplateEngine.BuildMessage(NEWS_EMAIL_NAME, await reader.ReadToEndAsync(), model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                }
            }
            return body;
        }

        private async Task<string> GetConvertedLogoImage(string filename)
        {
            string base64ImageRepresentation;
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", filename);
                byte[] imageArray = await File.ReadAllBytesAsync(path);
                base64ImageRepresentation = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
            }
            catch (Exception ex)
            {
                _logger.LogError(nameService + $": error encode image: {ex.Message}", ex);
                base64ImageRepresentation = "";
            }
            return base64ImageRepresentation;
        }

        private SmtpClient GetClient()
        {
            NetworkCredential credential = new(_settings.EmailSender.UserName, _settings.EmailSender.Password);
            if (!string.IsNullOrEmpty(_settings.EmailSender.Domain))
            {
                credential.Domain = _settings.EmailSender.Domain;
            }
            SmtpClient client = new(_settings.EmailSender.SmtpServer, _settings.EmailSender.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = credential,
                EnableSsl = _settings.EmailSender.UseSsl
            };
            return client;
        }

        private EmailNewsSubscriber Map(NewsSubscriber subscriber)
        {
            return new EmailNewsSubscriber()
            {
                FirstName = subscriber.FirstName,
                LastName = subscriber.LastName,
                Company = subscriber.Company,
                Email = subscriber.Email,
                NewsCategory = subscriber.NewsCategory
            };
        }
    }
}
