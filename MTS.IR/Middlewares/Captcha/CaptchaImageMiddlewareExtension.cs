using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.IR.Middlewares.Captcha
{
    public static class CaptchaImageMiddlewareExtension
    {
        public static IApplicationBuilder UseCaptchaImage(this IApplicationBuilder builder, string path)
        {
            return builder.Map(path, app => app.UseMiddleware<CaptchaImageMiddleware>());
        }
    }
}
