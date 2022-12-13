using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class ReportsService : IReportsService
    {
        private readonly PostgreQpDataContext _qpDataContext;
        public ReportsService(IDbContext context)
        {
            _qpDataContext = context as PostgreQpDataContext;
        }
        public IEnumerable<ReportDto> GetAllReports()
        {
            Report[] results = _qpDataContext.Reports.OrderByDescending(e => e.ReportDate)
                                               .Include(e => e.Files.OrderBy(e => e.SortOrder ?? 10))
                                               .ToArray();
            return results.Select(Map).ToArray();
        }
        public IEnumerable<ReportDto> GetReports(IEnumerable<int> idsReport)
        {
            Report[] results = _qpDataContext.Reports.Where(r => idsReport.Contains(r.Id))
                                               .OrderByDescending(e => e.ReportDate)
                                               .Include(e => e.Files.OrderBy(e => e.SortOrder ?? 10))
                                               .ToArray();
            return results.Select(Map).ToArray();
        }

        public ReportDto GetReport(int? id)
        {
            return id.HasValue ? Map(_qpDataContext.Reports.Include(r => r.Files).FirstOrDefault(r => r.Id == id.Value)) : null;

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
        private ReportFileDto Map(Postgre.DAL.ReportFile file)
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
