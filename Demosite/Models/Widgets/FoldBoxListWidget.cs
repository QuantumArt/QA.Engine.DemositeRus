using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace Demosite.Models.Widgets
{
    public class FoldBoxListWidget : AbstractWidget
    {
        public IEnumerable<int> FoldBoxListItemIds => GetRelationIds("FOLDBOXLISTITEMS");
        public string WidgetType => GetDetail<string>("WidgetType", null);
        public int? SlidesToShow => GetDetail<int?>("SlidesToShow", null);
    }
}
