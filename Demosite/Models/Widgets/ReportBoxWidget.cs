using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace Demosite.Models.Widgets
{
    public class ReportBoxWidget : AbstractWidget
    {
        public IEnumerable<int> ReportsItemIds => GetRelationIds("Reports");
        public int? CountReportToShow => GetDetail<int?>("CountCardToShow", null);
    }
}
