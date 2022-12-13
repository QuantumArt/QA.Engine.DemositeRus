using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    [Route("[controller]")]
    public class FeedbackController : Controller
    {
        private readonly FeedbackViewModelBuilder _modelBuilder;
        public FeedbackController(FeedbackViewModelBuilder builder)
        {
            _modelBuilder = builder;
        }

        [HttpPost("sendfeedback")]
        public async Task<IActionResult> SendFeedback([FromForm] FeedbackViewModel feedbackModel)
        {
            bool result = await _modelBuilder.SendFeedback(feedbackModel);
            return Redirect($"{Request.Scheme}://{Request.Host}/feedback/feedbacksended?result={result}");
        }
        [HttpGet("feedbacksended")]
        public IActionResult FeedbackSended([FromQuery] bool result)
        {
            ViewBag.IsConfirmed = result;
            return View();
        }
    }
}
