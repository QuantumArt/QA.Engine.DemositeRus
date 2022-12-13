using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Demosite.ViewModels.Builders
{
    public class FeedbackViewModelBuilder
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackViewModelBuilder(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<bool> SendFeedback(FeedbackViewModel model)
        {
            bool result = false;
            SmtpStatusCode status = await _feedbackService.SendFeedback(Map(model));
            if (status == SmtpStatusCode.Ok)
            {
                result = true;
            }
            return result;
        }

        private FeedbackDto Map(FeedbackViewModel feedback)
        {
            return new FeedbackDto()
            {
                EmailOrPhone = feedback.FeedbackMobileOrEmail,
                Name = feedback.FeedbackName,
                Commentary = feedback.FeedbackText
            };
        }
    }
}
