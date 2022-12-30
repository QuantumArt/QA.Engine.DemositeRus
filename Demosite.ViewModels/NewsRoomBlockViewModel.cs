using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class NewsRoomBlockViewModel
    {
        public string Title { get; set; }
        public List<NewsRoomBlockItemViewModel> Items { get; set; } = new();
        public string Url { get; set; }
    }
}
