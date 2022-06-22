using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IAnnualReportsService
    {
        IEnumerable<ReportDto> GetAllReports();
        IEnumerable<ReportDto> GetReports(IEnumerable<int> idsReport);
        ReportDto GetReport(int? id);
    }
}
