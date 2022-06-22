using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels
{
    public class FoldBoxListViewModel
    {
        public string WidgetType { get; set; }
        public List<FoldBoxListItemViewModel> Items { get; set; } = new List<FoldBoxListItemViewModel>();
        public string Header { get; set; }
        public int? SlidesToShow { get; set; }
    }

    public class FoldBoxListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

    }
}
