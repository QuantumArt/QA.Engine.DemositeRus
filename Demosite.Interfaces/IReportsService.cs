using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IReportsService
    {
        IEnumerable<ReportDto> GetAllReports();
        IEnumerable<ReportDto> GetReports(IEnumerable<int> idsReport);
        ReportDto GetReport(int? id);
    }
}
