using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTS.IR.Helpers;
using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Services.Settings;
using MTS.IR.ViewModels;
using MTS.IR.ViewModels.Builders;
using System;
using System.Threading.Tasks;

namespace MTS.IR.Controllers
{
    [Route("[controller]")]
    public class SubscribeController : Controller
    {
        private IEmailNotificationService _notificationService { get; }
        private SubscribeViewModelBuilder _subscribeViewModel { get; }
        public SubscribeController(IEmailNotificationService emailNotification,
                                   SubscribeViewModelBuilder modelBuilder)
        {
            this._notificationService = emailNotification;
            this._subscribeViewModel = modelBuilder;
        }

        [CaptchaValidation("TokenCaptcha")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SubscribeViewModel subscriber)
        {
            var result = await _subscribeViewModel.AddSubscriber(subscriber);
            if (!result.Success)
            {
                return new JsonResult(new { success = result.Success, typeError = result.TypeError, message = result.Message });
            }
            return new JsonResult(new { success = result.Success, message = $"{this.Request.Scheme}://{this.Request.Host}/CheckEmailInformation" });
        }
        [HttpGet("confirmedsubscribe")]
        public async Task<IActionResult> ConfirmedSubscribe(string confirmcode)
        {
            var result = await _notificationService.ConfirmedSubscribe(confirmcode);
            ViewBag.IsConfirmed = result.isConfirm;
            ViewBag.TextConfirmed = result.text;
            return View();
        }
        [HttpGet("unsubscribe")]
        public async Task<IActionResult> Unsubscribe(string confirmCode)
        {
            var result = await _notificationService.UnSubscribe(confirmCode);
            return View(result);
        }
    }
}
