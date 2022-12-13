using Demosite.Interfaces;
using RazorLight;
using System.Threading.Tasks;

namespace Demosite.Services
{
    public class NotificationTemplateEngine : INotificationTemplateEngine
    {
        private readonly RazorLightEngine _razorLight;

        public NotificationTemplateEngine(RazorLightEngine razorLight)
        {
            _razorLight = razorLight;
        }

        public async Task<string> BuildMessage<T>(string key, string template, T messageModel)
        {
            string result = "";
            RazorLight.Caching.TemplateCacheLookupResult cacheResult = _razorLight.Handler.Cache.RetrieveTemplate(key);
            if (cacheResult.Success)
            {
                ITemplatePage templatePage = cacheResult.Template.TemplatePageFactory();
                result = await _razorLight.RenderTemplateAsync(templatePage, messageModel);
            }
            else
            {
                result = await _razorLight.CompileRenderStringAsync<T>(
                key,
                template,
                messageModel);
            }
            return result;
        }
    }
}
