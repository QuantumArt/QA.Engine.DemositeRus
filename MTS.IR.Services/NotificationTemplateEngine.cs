using RazorLight;
using System.Threading.Tasks;
using MTS.IR.Interfaces;

namespace MTS.IR.Services
{
    public class NotificationTemplateEngine : INotificationTemplateEngine
    {
        public RazorLightEngine RazorLight { get; }

        public NotificationTemplateEngine(RazorLightEngine razorLight)
        {
            this.RazorLight = razorLight;
        }

        public async Task<string> BuildMessage<T>(string key, string template, T messageModel)
        {
            string result = "";
            var cacheResult = RazorLight.Handler.Cache.RetrieveTemplate(key);
            if (cacheResult.Success)
            {
                var templatePage = cacheResult.Template.TemplatePageFactory();
                result = await RazorLight.RenderTemplateAsync(templatePage, messageModel);
            }
            else
            {
                result = await RazorLight.CompileRenderStringAsync<T>(
                key,
                template,
                messageModel);
            }
            return result;
        }
    }
}
