using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Enums;
using Demosite.Interfaces.Dto.Request;
using Demosite.Postgre.DAL.NotQP;
using Demosite.Postgre.DAL.NotQP.Models;
using Demosite.Services.Models;
using Demosite.Services.Settings;
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
        private PostgreDataContext dbContext { get; }
        private INewsService _newsService { get; }
        private EmailNotificationSettings _settings { get; }
        private ILogger<EmailNotificationService> _logger { get; }
        private string nameService => nameof(EmailNotificationService);
        private string logoFileName = "";
        private INotificationTemplateEngine _notificationTemplateEngine { get; }
        private IWebHostEnvironment _environment { get; }
        public const string checkEmailName = "Demosite.Services.EmailTemplates.CheckEmail.cshtml";
        public const string newsEmailName = "Demosite.Services.EmailTemplates.NewsEmail.cshtml";
        public EmailNotificationService(IDbContextNotQP dbContext,
                                        INewsService newsService,
                                        EmailNotificationSettings settings,
                                        ILogger<EmailNotificationService> logger,
                                        INotificationTemplateEngine notificationTemplateEngine,
                                        IWebHostEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext as PostgreDataContext;
            this._newsService = newsService;
            this._settings = settings;
            this._logger = logger;
            this._notificationTemplateEngine = notificationTemplateEngine;
            this._environment = hostingEnvironment;
        }
        public async Task BackgroundSendEmails()
        {
            _logger.LogInformation(nameService + " is starting");
            var distributionId = await CreateDistribution();
            var distributionIdsNotCompleted = await CheckErrorDistribution();
            var isHaveNotCompleted = distributionIdsNotCompleted.Any();
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
            var distributionIdsNotCompleted = await CheckErrorDistribution();
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
            _logger.LogInformation(nameService + $": add new email to subscibe: {subscriber.Email}");
            subscriber.Email = subscriber.Email.ToLower();
            var newSubscriber = new EmailNewsSubscriber();
            if (subscriber.Email.Length == 0)
            {
                var message = "email address is required";
                _logger.LogInformation(nameService+ ": " + message);
                return new SubscriptionStatus()
                {
                    Success = false,
                    TypeError = "email",
                    Message = message
                };
            }
            if(subscriber.NewsCategory.Length == 0)
            {
                var message =  "news category is required";
                _logger.LogInformation(nameService +": " + message);
                return new SubscriptionStatus()
                {
                    Success = false,
                    TypeError = "newsCategory",
                    Message = message
                };
            }
            var existSubscriber = await dbContext.EmailNewsSubscribers.Where(s => s.Email == subscriber.Email)
                                                           .FirstOrDefaultAsync();
            if (existSubscriber != null && existSubscriber.IsActive)
            {
                var message = "this email address is already subscribed";
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
                dbContext.EmailNewsSubscribers.Add(newSubscriber);
            }
            try
            {
                await SendCheckEmail(newSubscriber);
            }
            catch (Exception ex)
            {
                _logger.LogError(nameService + $": error during send check email: {ex.Message}", ex);
            }
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return new SubscriptionStatus()
            {
                Success = true,
                Message = ""
            };
        }

        public async Task<(bool isConfirm, string text)> ConfirmedSubscribe(string confirmcode)
        {
            confirmcode = confirmcode.ToLower();
            var subscriber = await dbContext.EmailNewsSubscribers.FirstOrDefaultAsync(s => s.ConfirmCode == confirmcode);
            if (subscriber == null)
            {
                return (false, "We're sorry, but this subscription confirmation email is no longer valid. Please try again later.");
            }
            else if (subscriber != null && subscriber.IsActive)
            {
                return (false, "This email is already subscribed to news.");
            }
            else if(subscriber != null && subscriber.ConfirmCodeSendDate.HasValue && subscriber.ConfirmCodeSendDate.Value.Add(_settings.EmailConfirmationExpirationTime) < DateTime.Now)
            {
                return (false, "This email is already subscribed to news.");
            }
            subscriber.IsActive = true;
            await dbContext.SaveChangesAsync();
            return (true, "Your subscription has been confirmed.");
        }

        public async Task<SubscriptionStatus> UnSubscribe(string guid)
        {
            guid = guid.ToLower();
            var subscriber = await dbContext.EmailNewsSubscribers.FirstOrDefaultAsync(s => s.ConfirmCode == guid);
            if (subscriber == null)
            {
                var message = " Not find subscriber for unsubscribe";
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
            await dbContext.SaveChangesAsync();
            return new SubscriptionStatus()
            {
                Success = true,
                Message = "You will not receive any more emails with news"
            };
        }

        private async Task<int[]> CheckErrorDistribution()
        {
            var result = await dbContext.Distributions.Where(d => d.Status == SendStatus.Processing || d.Status == SendStatus.Error)
                                                      .Select(d => d.Id)
                                                      .ToArrayAsync();
            return result;
        }
        private async Task CreateAndSendEnvelope(EnvelopeParameterRequest request)
        {
            await CreateEnvelopes(request.DistributionId.Value);
            await SendEmailsToSubscribers(request);
        }

        private async Task SendCheckEmail(EmailNewsSubscriber subscriber)
        {
            
            string base64ImageRepresentation = await GetConvertedLogoImage(logoFileName);
            string body = "";
            var model = new EmailModel()
            {
                Subscriber = new Subscriber(),
                NewsPosts = new NewsPostDto[0],
                BaseUrl = _settings.BaseURLNewsService + @"/subscribe/confirmedsubscribe?confirmcode=" + subscriber.ConfirmCode,
                LogoImage = base64ImageRepresentation
            };
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(checkEmailName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    try
                    {
                        body = await _notificationTemplateEngine.BuildMessage(checkEmailName, await reader.ReadToEndAsync(), model);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                    }
                }
            }
            var envelope = new Envelope()
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
                    await SendEmailAsync(new[] { envelope }, SmtpClient, "Email Confirmation", false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameService + $": error during send email with check code: {ex.Message}", ex);
                }
            }
        }

        private async Task<int?> CreateDistribution()
        {
            try
            {
                _logger.LogInformation(nameService + $": creating a list of news to send");
                //получить дату последней рассылки
                DateTime lastDateSend = DateTime.Now.Subtract(_settings.SendTimeInterval);
                //получить новые новости с даты последней расылки
                var newNews = _newsService.GetAllPosts(new PostRequest()
                {
                    FromDate = lastDateSend,
                });
                if (!newNews.Any())
                {
                    _logger.LogInformation(nameService + $": not found new news");
                    return null;
                }
                var newDistribution = new Distribution()
                {
                    NewsIds = newNews.Select(n => n.Id).ToArray(),
                    Status = SendStatus.Created,
                    Created = DateTime.Now
                };
                dbContext.Distributions.Add(newDistribution);
                await dbContext.SaveChangesAsync();
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
            var distribution = await dbContext.Distributions
                                              .FirstOrDefaultAsync(d => d.Id == distributionId);
            if (distribution == null)
            {
                _logger.LogInformation(nameService + $": not found distribution with ID: {distributionId}");
                return;
            }
            distribution.Status = SendStatus.Processing;
            await dbContext.SaveChangesAsync();
            var news = (_newsService.GetPosts(distribution.NewsIds))
                                   .OrderByDescending(n => n.PostDate)
                                   .ToArray();
            if (news.Length == 0)
            {
                _logger.LogInformation(nameService + $": not found new news to send");
                return;
            }
            string base64ImageRepresentation = await GetConvertedLogoImage(logoFileName);
            var query = dbContext.EmailNewsSubscribers
                        .AsNoTracking()
                        .Where(s => s.IsActive);
            //для поиска недосозданных рассылок в рамках текущего distribution
            int? lastSendIdSubscriber = await dbContext.Envelopes.Where(e => e.DistributionId == distributionId)
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
                List<Envelope> envelopes = new List<Envelope>(subscribers.Length);
                foreach (var subscriber in subscribers)
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
                        await dbContext.SaveChangesAsync();
                        return;
                    }
                }
                dbContext.Envelopes.AddRange(envelopes);
                await dbContext.SaveChangesAsync();
                _logger.LogInformation(nameService + $": new envelopes was saved");
            };
            distribution.Status = SendStatus.Sending;
            await dbContext.SaveChangesAsync();
        }

        private async Task SendEmailsToSubscribers(EnvelopeParameterRequest request)
        {
            _logger.LogInformation(nameService + $": start sending mail");
            var predicate = PredicateBuilder.New<Envelope>(true);
            predicate = predicate.And(e => e.Status == SendStatus.Created);
            if (request.IncludeEnvelopeNotSending)
            {
                predicate = predicate.Or(e => e.Status == SendStatus.Error);
            }
            var queryEnvelopes = dbContext.Envelopes.Where(predicate);
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
            using (SmtpClient SmtpClient = GetClient())
            {
                for (int i = 0; i < envelopesLength; i += _settings.MailingPacketSize)
                {
                    var envelopes = await queryEnvelopes.Skip(i)
                                         .Take(_settings.MailingPacketSize)
                                         .Include(e => e.Subscriber)
                                         .ToArrayAsync();
                    if (envelopes.Length == 0)
                    {
                        break;
                    }
                    var distribution = await dbContext.Distributions.FirstOrDefaultAsync(d => d.Id == envelopes[0].DistributionId);
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
                        (SmtpStatusCode status, int distributionId) result = await SendEmailAsync(envelopes, SmtpClient, _settings.Subject);
                        if (distribution != null)
                        {
                            if (result.status == SmtpStatusCode.ServiceNotAvailable)
                            {
                                distribution.Status = SendStatus.Error;
                            }
                            else
                            {
                                distribution.Status = SendStatus.Completed;
                            }
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
                    await dbContext.SaveChangesAsync();
                }
            }   
        }
        private async Task<(SmtpStatusCode status, int distributionId)> SendEmailAsync(Envelope[] envelopes, SmtpClient client, string subject, bool saveContext = true)
        {
            (SmtpStatusCode status, int distributionId) result = (SmtpStatusCode.Ok, -1);
            //рассылка писем

            foreach (var envelope in envelopes)
            {
                var message = new MailMessage();
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
                    await dbContext.SaveChangesAsync();
                }
            }
            result = (SmtpStatusCode.Ok, envelopes[0].DistributionId);

            return result;
        }

        private async Task<string> CreateBodyEmail(NewsPostDto[] newsArray, EmailNewsSubscriber subscriber, string logoImageBASE64)
        {
            string body = "";
            var model = new EmailModel()
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
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(newsEmailName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    try
                    {
                        body = await _notificationTemplateEngine.BuildMessage(newsEmailName, await reader.ReadToEndAsync(), model);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                    }
                }
            }
            return body;
        }

        private async Task<string> GetConvertedLogoImage(string filename)
        {
            string base64ImageRepresentation = "";
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", filename);
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
            var credential = new NetworkCredential(this._settings.EmailSender.UserName, this._settings.EmailSender.Password);
            if(!string.IsNullOrEmpty(this._settings.EmailSender.Domain))
            {
                credential.Domain = this._settings.EmailSender.Domain;
            }
            var client = new SmtpClient(this._settings.EmailSender.SmtpServer, this._settings.EmailSender.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = credential,
                EnableSsl = this._settings.EmailSender.UseSsl
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
