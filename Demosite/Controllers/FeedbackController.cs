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
        public async Task<IActionResult> SendFeedback([FromBody] FeedbackViewModel feedback)
        {
            bool result = await _modelBuilder.SendFeedback(feedback);
            return new JsonResult(new { success = result, message = $"{this.Request.Scheme}://{this.Request.Host}/feedback/feedbacksended?result={result}" });
        }
        [HttpGet("feedbacksended")]
        public async Task<IActionResult> FeedbackSended([FromQuery] bool result)
        {
            ViewBag.IsConfirmed = result;
            return View();
        }
    }
}
