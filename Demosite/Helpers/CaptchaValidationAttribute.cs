using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Demosite.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Demosite.ViewModels.Interface;

namespace Demosite.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CaptchaValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaCheckAttribute"/> class.
        /// </summary>
        public CaptchaValidationAttribute()
            : this("captcha") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaCheckAttribute"/> class.
        /// </summary>
        /// <param name="field">Name of the field where user enters captcha.</param>
        public CaptchaValidationAttribute(string field)
        {
            Field = field;
        }

        /// <summary>
        /// Gets or sets the field with the entered captch.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; private set; }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var settings = filterContext.HttpContext.RequestServices.GetService<CaptchaSettings>();
            if(!settings.CaptchaIsActive)
            {
                return;
            }
            bool isCaptchaValid = false;
            string key = settings.DefaultKey;
            string actualValue = "";
            ICaptchaModel model = filterContext.ActionArguments.First().Value as ICaptchaModel;
            if (filterContext.HttpContext.Session != null)
            {
                string keyForm = model.CaptchaKey;
                if (!string.IsNullOrEmpty(keyForm))
                {
                    key = keyForm;
                }
                actualValue = model.TokenCaptcha;
                string expectedValue = filterContext.HttpContext.Session.GetString(key);

                isCaptchaValid = !String.IsNullOrEmpty(actualValue)
                              && !String.IsNullOrEmpty(expectedValue)
                              && String.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase);

                filterContext.HttpContext.Session.Remove(key);
            }
            if(!isCaptchaValid)
            {
                var output = new JsonResult(new { success = false, typeError = "captcha", message = "Your Captcha response was incorrect. Please try again." });
                filterContext.Result = output;
            }
        }
    }
}
