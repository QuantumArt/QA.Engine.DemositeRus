using Demosite.Interfaces.Models;
using System;

namespace Demosite.Interfaces.Dto.Request
{
    public class PostRequest
    {
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public bool? IsPublished { get; set; }
        public ArrayFilter<int> NewsIds { get; set; }
        public ArrayFilter<int> CategoryIds { get; set; }
    }
}
