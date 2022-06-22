using MTS.IR.Interfaces.Dto;
using MTS.IR.Interfaces.Dto.Request;
using System.Threading.Tasks;

namespace MTS.IR.Interfaces
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
