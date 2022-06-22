using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL.NotQP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTS.IR.Services
{
    public class WarmUp :IWarmUp
    {
        private INotificationTemplateEngine _engine { get; }
        private ILogger<WarmUp> _logger { get; }
        private IServiceProvider _serviceProvider { get; }
        private string nameService => nameof(WarmUp);
        public WarmUp(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        public async Task WarmpUpEmail(CancellationToken cancellationToken = default)
        {
            var model = new EmailModel()
            {
                Subscriber = new Subscriber(),
                NewsPosts = new NewsPostDto[0],
                BaseUrl = "",
                LogoImage = ""
            };
            using (var scope = _serviceProvider.CreateScope())
            {
                var engine = scope.ServiceProvider.GetService<INotificationTemplateEngine>();
                var logger = scope.ServiceProvider.GetService<ILogger<WarmUp>>();
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmailNotificationService.checkEmailName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        try
                        {
                            await engine.BuildMessage(EmailNotificationService.checkEmailName, await reader.ReadToEndAsync(), model);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(nameService + $": error RazorLight during create email body: {ex.Message}", ex);
                        }
                    }
                }
            }    
        }
    }
}
