using System;

namespace Demosite.ViewModels
{
    public class NewsRoomBlockItemViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTimeOffset? PostData { get; set; }
        public string Brief { get; set; }
    }
}
