using System;
using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class NewsPageViewModel
    {
        public string Header { get; set; }
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new();
        public List<NewsItemInListViewModel> Items { get; set; } = new();
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public Dictionary<int, int[]> DateDictionary { get; set; } = new();
    }
}
