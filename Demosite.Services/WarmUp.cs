using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL.NotQP.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Services
{
    public class WarmUp : IWarmUp
    {
        private readonly IServiceProvider _serviceProvider;
        private string nameService => nameof(WarmUp);
        public WarmUp(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task WarmpUpEmail(CancellationToken cancellationToken = default)
        {
            EmailModel model = new()
            {
                Subscriber = new Subscriber(),
                NewsPosts = new NewsPostDto[0],
                BaseUrl = "",
                LogoImage = ""
            };
            using IServiceScope scope = _serviceProvider.CreateScope();
            INotificationTemplateEngine engine = scope.ServiceProvider.GetService<INotificationTemplateEngine>();
            ILogger<WarmUp> logger = scope.ServiceProvider.GetService<ILogger<WarmUp>>();
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmailNotificationService.CHECK_EMAIL_NAME);
            using StreamReader reader = new(stream);
            try
            {
                await engine.BuildMessage(EmailNotificationService.CHECK_EMAIL_NAME, await reader.ReadToEndAsync(), model);
            }
            catch (Exception ex)
            {
                logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
            }
        }
    }
}
