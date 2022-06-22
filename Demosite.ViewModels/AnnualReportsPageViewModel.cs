using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class AnnualReportsPageViewModel
    {
        public string Title { get; set; }
        public List<ReportItem> Reports { get; set; } = new List<ReportItem>();
        public int? CountReportsToShow { get; set; }
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
    }

    public class ReportItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string AdditionalAttachedImageUrl { get; set; }
        public List<ReportFileItem> Files { get; set; } = new List<ReportFileItem>();
    }

    public class ReportFileItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SortOrder { get; set; }
        public string FileUrl { get; set; }
    }
}
