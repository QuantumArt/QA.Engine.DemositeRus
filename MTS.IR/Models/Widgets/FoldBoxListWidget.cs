using QA.DotNetCore.Engine.QpData;
using MTS.IR.Postgre.DAL;
using System.Collections.Generic;
using MTS.IR.Interfaces.Dto.Enums;

namespace MTS.IR.Models.Widgets
{
    public class FoldBoxListWidget: AbstractWidget
    {
        public IEnumerable<int> FoldBoxListItemIds => GetRelationIds("FOLDBOXLISTITEMS");
        public string WidgetType => GetDetail<string>("WidgetType", null);
        public int? SlidesToShow => GetDetail<int?>("SlidesToShow", null);
    }
}
