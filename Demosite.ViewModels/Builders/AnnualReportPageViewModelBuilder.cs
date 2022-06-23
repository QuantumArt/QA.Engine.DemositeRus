using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class AnnualReportPageViewModelBuilder
    {
        private IReportsService reportsService { get; }
        public AnnualReportPageViewModelBuilder(IReportsService service)
        {
            this.reportsService = service;
        }
        public AnnualReportsPageViewModel BuildForm(IAbstractPage page, IEnumerable<int> ids)
        {
            var vm = new AnnualReportsPageViewModel() { Title = page.Title };
            var reports = reportsService.GetReports(ids).Select(Map).ToArray();
            vm.Reports.AddRange(reports);
            vm.BreadCrumbs = page.GetBreadCrumbs();
            return vm;
        }
        private ReportItem Map(ReportDto model)
        {
            return new ReportItem()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.Image,
                AdditionalAttachedImageUrl = model.AdditionalAttachedImageUrl,
                Files = model.ReportFiles.Select(Map).ToList()
            };
        }
        private ReportFileItem Map (ReportFileDto file)
        {
            return new ReportFileItem()
            {
                Id = file.Id,
                Title = file.Title,
                SortOrder = file.SortOrder,
                FileUrl = file.FileUrl
            };
        }
    }
}
