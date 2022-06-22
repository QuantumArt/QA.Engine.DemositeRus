using Microsoft.EntityFrameworkCore;
using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Services
{
    public class AnnualReportsService: IAnnualReportsService
    {
        private PostgreQpDataContext qpDataContext { get; }
        public AnnualReportsService(IDbContext context)
        {
            qpDataContext = context as PostgreQpDataContext;
        }
        public IEnumerable<ReportDto> GetAllReports()
        {
            var results = qpDataContext.Reports.OrderByDescending(e => e.ReportDate)
                                               .Include(e => e.Files.OrderBy(e => e.SortOrder ?? 10))
                                               .ToArray();
            return results.Select(Map).ToArray();
        }
        public IEnumerable<ReportDto> GetReports(IEnumerable<int> idsReport)
        {
            var results = qpDataContext.Reports.Where(r => idsReport.Contains(r.Id))
                                               .OrderByDescending(e => e.ReportDate)
                                               .Include(e => e.Files.OrderBy(e => e.SortOrder ?? 10))
                                               .ToArray();
            return results.Select(Map).ToArray();
        }

        public ReportDto GetReport(int? id)
        {
            if(id.HasValue)
            {
                return Map(qpDataContext.Reports.FirstOrDefault(r => r.Id == id.Value));
            }
            else
            {
                return null;
            }
            
        }

        private ReportDto Map(Postgre.DAL.Report model)
        {
            return new ReportDto()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.ImageUrl,
                AdditionalAttachedImageUrl = model.AdditionalAttachedImageUrl,
                ReportFiles = model.Files.Select(Map).ToArray()
            };
        }
        private ReportFileDto Map (Postgre.DAL.ReportFile file)
        {
            return new ReportFileDto()
            {
                Id = file.Id,
                Title = file.Title,
                SortOrder = file.SortOrder,
                FileUrl = file.FileUrl
            };
        }

        
    }
}
