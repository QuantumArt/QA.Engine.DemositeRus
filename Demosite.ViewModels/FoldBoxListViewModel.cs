using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class FoldBoxListViewModel
    {
        public string WidgetType { get; set; }
        public List<FoldBoxListItemViewModel> Items { get; set; } = new();
        public string Header { get; set; }
        public int? SlidesToShow { get; set; }
    }
}
