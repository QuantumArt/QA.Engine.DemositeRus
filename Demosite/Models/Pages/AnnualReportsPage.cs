using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace Demosite.Models.Pages
{
    public class AnnualReportsPage : AbstractPage
    {
        public int? ReportItemId => GetDetail<int?>("ReportItem", null);
    }
}
