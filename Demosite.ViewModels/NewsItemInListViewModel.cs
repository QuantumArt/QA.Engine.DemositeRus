using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class NewsItemInListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string CategoryName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Url { get; set; }
        public bool Published { get; set; }
    }
}
