using System.Threading.Tasks;

namespace Demosite.Interfaces
{
    public interface INotificationTemplateEngine
    {
        Task<string> BuildMessage<T>(string key, string template, T messageModel);
    }
}
