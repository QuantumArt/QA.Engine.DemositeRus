using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class MediaPageViewModel
    {
        public string Title { get; set; }
        public List<EventItem> Events { get; set; } = new List<EventItem>();
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
    }

    public class EventItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string TextBelow { get; set; }
        public List<EventImageItem> Images { get; set; } = new List<EventImageItem>();
    }
    public class EventImageItem
    {
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public string ImageURL { get; set; }
    }
}
