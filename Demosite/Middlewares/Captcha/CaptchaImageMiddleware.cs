using Demosite.Services.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLaborsCaptcha.Core;
using System;
using System.Threading.Tasks;

namespace Demosite.Middlewares.Captcha
{
    public class CaptchaImageMiddleware
    {
        public CaptchaImageMiddleware(RequestDelegate next)
        { }

        public async Task Invoke(HttpContext context, CaptchaSettings captchaSettings, ISixLaborsCaptchaModule sixLaborsCaptcha, ILogger<CaptchaImageMiddleware> logger)
        {
            context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };

            if (captchaSettings.Referrer.Length > 0 &&
                (string.IsNullOrEmpty(context.Request.Headers["Referer"]) ||
                 context.Request.Headers["Referer"].ToString().IndexOf(captchaSettings.Referrer, StringComparison.Ordinal) < 0))
            {
                NotFound(context);
                return;
            }

            string key = "mamimagemd5hash";
            if (!string.IsNullOrEmpty(context.Request.Query["key"]))
            {
                key = context.Request.Query["key"];
            }

            string captcha_key = Extensions.GetUniqueKey(captchaSettings.TextLength);
            // Без сессий мы нарисуем капчу, но не сможем ее проверить. Любое значение будет отвергаться.
            context.Session?.SetString(key, captcha_key);
            context.Response.ContentType = "image/gif";
            context.Response.StatusCode = 200;
            try
            {
                byte[] imgText = sixLaborsCaptcha.Generate(captcha_key);
                await context.Response.Body.WriteAsync(imgText, 0, imgText.Length);
            }
            catch (Exception ex)
            {
                logger.LogError("captcha error: " + ex.Message);
            }
        }

        private void NotFound(HttpContext context)
        {
            context.Response.StatusCode = 404;
        }
    }
}
