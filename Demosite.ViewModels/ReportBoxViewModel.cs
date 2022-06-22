using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class ReportBoxViewModel
    {
        public string Title { get; set; }
        public List<ReportItem> Reports { get; set; } = new List<ReportItem>();
        public int? CountReportsToShow { get; set; }
    }
}
