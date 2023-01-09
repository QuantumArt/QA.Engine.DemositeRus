using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class MediaPageViewModel
    {
        public string Title { get; set; }
        public List<MediaPageEventItem> Events { get; set; } = new();
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new();
    }
}
