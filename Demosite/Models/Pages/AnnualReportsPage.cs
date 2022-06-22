using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace Demosite.Models.Pages
{
    public class AnnualReportsPage : AbstractPage
    {
        public IEnumerable<int> ReportsItemIds => GetRelationIds("Reports");
        public int? CountReportToShow => GetDetail<int?>("CountCardToShow", null);
    }
}
