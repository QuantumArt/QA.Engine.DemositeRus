using Demosite.Interfaces.Dto;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Demosite.Interfaces
{
    public interface IFeedbackService
    {
        Task<SmtpStatusCode> SendFeedback(FeedbackDto model);
    }
}
