using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class NewsDetailsViewModel
    {
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string AllnewsUrl { get; set; }
        public string CommonText { get; set; }
        public int Id { get; set; }
    }
}
