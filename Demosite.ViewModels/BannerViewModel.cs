using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class BannerListViewModel
    {
        public List<BannerItemViewModel> Items { get; set; } = new();
        public int? SwipeDelay { get; set; }
        public string Title { get; set; }
    }
}
