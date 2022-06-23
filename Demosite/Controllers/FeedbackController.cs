using Microsoft.AspNetCore.Mvc;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    [Route("[controller]")]
    public class FeedbackController : Controller
    {
        private FeedbackViewModelBuilder _modelBuilder { get; }
        public FeedbackController(FeedbackViewModelBuilder builder)
        {
            this._modelBuilder = builder;
        }
        
        [HttpPost("sendfeedback")]
        public async Task<IActionResult> SendFeedback([FromForm] FeedbackViewModel feedbackModel)
        {
            bool result = await _modelBuilder.SendFeedback(feedbackModel);
            return Redirect($"{this.Request.Scheme}://{this.Request.Host}/feedback/feedbacksended?result={result}");
        }
        [HttpGet("feedbacksended")]
        public async Task<IActionResult> FeedbackSended([FromQuery] bool result)
        {
            ViewBag.IsConfirmed = result;
            return View();
        }
    }
}
