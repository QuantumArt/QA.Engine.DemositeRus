using System;
using System.Collections.Generic;

namespace MTS.IR.ViewModels
{
    public class MeetingDetailsViewModel
    {
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
        public string AllMeetingsUrl { get; set; }
    }
}
