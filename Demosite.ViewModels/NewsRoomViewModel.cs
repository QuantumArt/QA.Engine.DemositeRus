using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class NewsRoomViewModel
    {
        public string Title { get; set; }
        public List<NewsRoomBlockViewModel> Blocks { get; set; } = new();
    }
}
