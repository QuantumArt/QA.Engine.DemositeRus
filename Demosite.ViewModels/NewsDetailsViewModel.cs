using System;
using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class NewsDetailsViewModel
    {
        public List<BreadCrumbViewModel> BreadCrumbs { get; set; } = new();
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Text { get; set; }
        public string AllnewsUrl { get; set; }
        public string CommonText { get; set; }
        public int Id { get; set; }
    }
}
