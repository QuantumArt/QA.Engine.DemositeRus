using System.Collections.Generic;

namespace MTS.IR.ViewModels
{
    public class BannerListViewModel
    {
        public List<BannerItemViewModel> Items { get; set; } = new List<BannerItemViewModel>();
        public int? SwipeDelay { get; set; }
        public string Title { get; set; }
    }

    public class BannerItemViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public string ImageUrl { get; set; }
    }
}
