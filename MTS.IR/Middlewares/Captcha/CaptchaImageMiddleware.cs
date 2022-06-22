using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MTS.IR.Helpers;
using MTS.IR.Services;
using MTS.IR.Services.Settings;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace MTS.IR.Middlewares.Captcha
{
    public class CaptchaImageMiddleware
    {
        public CaptchaImageMiddleware(RequestDelegate next)
        { }

        public async Task Invoke(HttpContext context, CaptchaSettings captchaSettings)
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

            var ci = new CaptchaImage(captchaSettings);
            if (context.Request.Query.ContainsKey("w"))
            {
                int.TryParse(context.Request.Query["w"], out int _width);
                ci.Width = _width;
            }
            if (context.Request.Query.ContainsKey("h"))
            {
                int.TryParse(context.Request.Query["h"], out int _height);
                ci.Height = _height;
            }
            string text = ci.Text;

            string key = "mamimagemd5hash";
            if (!string.IsNullOrEmpty(context.Request.Query["key"]))
            {
                key = context.Request.Query["key"];
            }

            // Без сессий мы нарисуем капчу, но не сможем ее проверить. Любое значение будет отвергаться.
            if (context.Session != null)
            {
                context.Session.SetString(key, text);
            }
            context.Response.ContentType = "image/gif";
            context.Response.StatusCode = 200;
            using (Bitmap b = ci.RenderImage())
            {
                var bytes = b.ToByteArray(ImageFormat.Gif);
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            } 
        }

        private void NotFound(HttpContext context)
        {
            context.Response.StatusCode = 404;
        }
    }
}
