using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using System.Threading.Tasks;

namespace Demosite.Interfaces
{
    public interface IEmailNotificationService
    {
        Task CheckIncompleledDistributions();
        Task BackgroundSendEmails();
        Task<SubscriptionStatus> AddSubscriber(NewsSubscriber subscriber);
        Task<SubscriptionStatus> UnSubscribe(string guid);
        Task<(bool isConfirm, string text)> ConfirmedSubscribe(string guid);
    }
}
