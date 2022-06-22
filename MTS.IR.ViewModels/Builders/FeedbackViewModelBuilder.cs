using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MTS.IR.ViewModels.Builders
{
    public class FeedbackViewModelBuilder
    {
        private IFeedbackService _feedbackService { get; }
        public FeedbackViewModelBuilder(IFeedbackService feedbackService)
        {
            this._feedbackService = feedbackService;
        }
        public async Task<bool> SendFeedback(FeedbackViewModel model)
        {
            bool result = false;
            SmtpStatusCode status = await _feedbackService.SendFeedback(Map(model));
            if(status == SmtpStatusCode.Ok)
            {
                result = true;
            }
            return result;
        }

        private FeedbackDto Map (FeedbackViewModel feedback)
        {
            return new FeedbackDto()
            {
                Email = feedback.Email,
                Name = feedback.Name,
                Phone = feedback.PhoneNumber,
                Commentary = feedback.Commentary
            };
        }
    }
}
