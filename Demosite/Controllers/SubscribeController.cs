using Demosite.Helpers;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto.Request;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    [Route("[controller]")]
    public class SubscribeController : Controller
    {
        private readonly IEmailNotificationService _notificationService;
        private readonly SubscribeViewModelBuilder _subscribeViewModel;
        public SubscribeController(IEmailNotificationService emailNotification,
                                   SubscribeViewModelBuilder modelBuilder)
        {
            _notificationService = emailNotification;
            _subscribeViewModel = modelBuilder;
        }

        [CaptchaValidation("TokenCaptcha")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SubscribeViewModel subscriber)
        {
            SubscriptionStatus result = await _subscribeViewModel.AddSubscriber(subscriber);
            return !result.Success
                ? new JsonResult(new { success = result.Success, typeError = result.TypeError, message = result.Message })
                : (IActionResult)new JsonResult(new { success = result.Success, message = $"{Request.Scheme}://{Request.Host}/CheckEmailInformation" });
        }
        [HttpGet("confirmedsubscribe")]
        public async Task<IActionResult> ConfirmedSubscribe(string confirmcode)
        {
            (bool isConfirm, string text) = await _notificationService.ConfirmedSubscribe(confirmcode);
            ViewBag.IsConfirmed = isConfirm;
            ViewBag.TextConfirmed = text;
            return View();
        }
        [HttpGet("unsubscribe")]
        public async Task<IActionResult> Unsubscribe(string confirmCode)
        {
            SubscriptionStatus result = await _notificationService.UnSubscribe(confirmCode);
            return View(result);
        }
    }
}
