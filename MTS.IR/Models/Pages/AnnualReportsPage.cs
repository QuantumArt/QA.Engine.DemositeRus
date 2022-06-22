using QA.DotNetCore.Engine.QpData;
using System.Collections.Generic;

namespace MTS.IR.Models.Pages
{
    public class AnnualReportsPage : AbstractPage
    {
        public int? ReportItemId => GetDetail<int?>("ReportItem", null);
    }
}
