using MTS.IR.Interfaces.Dto;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IAnnualReportsService
    {
        IEnumerable<ReportDto> GetAllReports();
        IEnumerable<ReportDto> GetReports(IEnumerable<int> idsReport);
        ReportDto GetReport(int? id);
    }
}
