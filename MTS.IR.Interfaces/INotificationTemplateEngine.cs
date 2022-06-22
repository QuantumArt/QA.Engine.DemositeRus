using System.Threading.Tasks;

namespace MTS.IR.Interfaces
{
    public interface INotificationTemplateEngine
    {
        Task<string> BuildMessage<T>(string key, string template, T messageModel);
    }
}
