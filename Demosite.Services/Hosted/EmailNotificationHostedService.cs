using Demosite.Interfaces;
using Demosite.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Services.Hosted
{
    public class EmailNotificationHostedService : BackgroundService
    {
        private readonly ILogger<EmailNotificationHostedService> _logger;
        private readonly Timer _timer;
        private readonly EmailNotificationSettings _settings;
        private readonly IServiceProvider _serviceProvider;
        public EmailNotificationHostedService(ILogger<EmailNotificationHostedService> logger,
                                              EmailNotificationSettings settings,
                                              IServiceProvider serviceProvider)
        {
            _logger = logger;
            _settings = settings;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("SendNewsEmailService background task  is starting");
            stoppingToken.Register(() =>
            _logger.LogDebug($" SendNewsEmailService background task is stopping."));

            using IServiceScope scope = _serviceProvider.CreateScope();
            IWarmUp warmService = scope.ServiceProvider.GetService<IWarmUp>();
            await warmService.WarmpUpEmail(stoppingToken);
            IEmailNotificationService notificationService = scope.ServiceProvider.GetService<IEmailNotificationService>();
            await notificationService.CheckIncompleledDistributions();
            TimeSpan startThrough = GetIntervalTime();
            await Task.Delay(startThrough);
            while (!stoppingToken.IsCancellationRequested)
            {
                notificationService.BackgroundSendEmails();
                await Task.Delay(_settings.SendTimeInterval, stoppingToken);
            }
            _logger.LogDebug($"SendNewsEmailService background task is stopping.");
        }

        private TimeSpan GetIntervalTime()
        {
            DateTime now = DateTime.Now;
            DateTime startTime = new(now.Year, now.Month, now.Day + _settings.SendTimeStart.Days, _settings.SendTimeStart.Hours, _settings.SendTimeStart.Minutes, _settings.SendTimeStart.Seconds);
            return startTime > now ? startTime - now : startTime.AddDays(1) - now;
        }
    }
}
