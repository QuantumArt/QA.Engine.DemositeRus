using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class MeetingPageViewModel
    {
        public string Header { get; set; }
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
        public List<MeetingPageItemInListViewModel> Items { get; set; } = new List<MeetingPageItemInListViewModel>();
    }

    public class MeetingPageItemInListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string Text { get; set; }
        public bool IsArchive { get; set; }
        public string URL { get; set; }
    }
}
