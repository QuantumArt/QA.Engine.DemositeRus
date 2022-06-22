using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace MTS.IR.Models.Widgets
{
    public class ReportBoxWidget : AbstractWidget
    {
        public IEnumerable<int> ReportsItemIds => GetRelationIds("Reports");
        public int? CountReportToShow => GetDetail<int?>("CountCardToShow", null);
    }
}
