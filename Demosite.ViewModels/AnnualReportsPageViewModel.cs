using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class AnnualReportsPageViewModel
    {
        public string Title { get; set; }
        public List<AnnualReportsItem> Reports { get; set; } = new();
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new();
    }
}
