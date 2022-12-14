using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class NewsPageViewModel
    {
        public string Header { get; set; }
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new List<BreadCrumbViewModel>();
        public List<NewsItemInListViewModel> Items { get; set; } = new List<NewsItemInListViewModel>();
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }

    public class NewsItemInListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }

        public bool Published { get; set; }
    }
}
