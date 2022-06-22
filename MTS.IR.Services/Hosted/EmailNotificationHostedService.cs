using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MTS.IR.Interfaces;
using MTS.IR.Services.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MTS.IR.Services.Hosted
{
    public class EmailNotificationHostedService : BackgroundService
    {
        private ILogger<EmailNotificationHostedService> _logger { get; }
        private Timer _timer { get; set; }
        private EmailNotificationSettings _settings { get; }
        private IServiceProvider _serviceProvider { get; }
        public EmailNotificationHostedService(ILogger<EmailNotificationHostedService> logger,
                                              EmailNotificationSettings settings,
                                              IServiceProvider serviceProvider)
        {
            this._logger = logger;
            this._settings = settings;
            this._serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("SendNewsEmailService background task  is starting");
            stoppingToken.Register(() =>
            _logger.LogDebug($" SendNewsEmailService background task is stopping."));
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var warmService = scope.ServiceProvider.GetService<IWarmUp>();
                await warmService.WarmpUpEmail(stoppingToken);
                var notificationService = scope.ServiceProvider.GetService<IEmailNotificationService>();
                await notificationService.CheckIncompleledDistributions();
                var startThrough = GetIntervalTime();
                await Task.Delay(startThrough);
                while (!stoppingToken.IsCancellationRequested)
                {
                    notificationService.BackgroundSendEmails();
                    await Task.Delay(_settings.SendTimeInterval, stoppingToken);
                }
                _logger.LogDebug($"SendNewsEmailService background task is stopping.");
            }
        }

        private TimeSpan GetIntervalTime ()
        {
            DateTime now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, now.Day + _settings.SendTimeStart.Days, _settings.SendTimeStart.Hours, _settings.SendTimeStart.Minutes, _settings.SendTimeStart.Seconds);
            return startTime > now ? startTime - now : startTime.AddDays(1) - now;
        }
    }
}
