using System;

namespace Demosite.Services.Settings
{
    public class HttpCacheControl
    {
        public TimeSpan MaxAge { get; set; }
        public TimeSpan StaticFilesMaxAge { get; set; }
    }
}
