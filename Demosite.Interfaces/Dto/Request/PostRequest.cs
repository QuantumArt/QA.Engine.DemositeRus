using System;

namespace Demosite.Interfaces.Dto.Request
{
    public class PostRequest
    {
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
