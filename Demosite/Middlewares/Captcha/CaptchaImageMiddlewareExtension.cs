using Microsoft.AspNetCore.Builder;

namespace Demosite.Middlewares.Captcha
{
    public static class CaptchaImageMiddlewareExtension
    {
        public static IApplicationBuilder UseCaptchaImage(this IApplicationBuilder builder, string path)
        {
            return builder.Map(path, app => app.UseMiddleware<CaptchaImageMiddleware>());
        }
    }
}
