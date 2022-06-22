using MTS.IR.Interfaces.Dto;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MTS.IR.Interfaces
{
    public interface IFeedbackService
    {
        Task<SmtpStatusCode> SendFeedback(FeedbackDto model);
    }
}
