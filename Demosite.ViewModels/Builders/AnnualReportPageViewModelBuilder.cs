using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class AnnualReportPageViewModelBuilder
    {
        private readonly IReportsService _reportsService;
        public AnnualReportPageViewModelBuilder(IReportsService service)
        {
            _reportsService = service;
        }
        public AnnualReportsPageViewModel BuildForm(IAbstractPage page, IEnumerable<int> ids)
        {
            AnnualReportsPageViewModel viewModel = new() { Title = page.Title };
            AnnualReportsItem[] reports = _reportsService.GetReports(ids).Select(Map).ToArray();
            viewModel.Reports.AddRange(reports);
            viewModel.BreadCrumbs = page.GetBreadCrumbs();
            return viewModel;
        }
        private AnnualReportsItem Map(ReportDto model)
        {
            return new AnnualReportsItem()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.Image,
                AdditionalAttachedImageUrl = model.AdditionalAttachedImageUrl,
                Files = model.ReportFiles.Select(Map).ToList()
            };
        }
        private AnnualReportsFileItem Map(ReportFileDto file)
        {
            return new AnnualReportsFileItem()
            {
                Id = file.Id,
                Title = file.Title,
                SortOrder = file.SortOrder,
                FileUrl = file.FileUrl
            };
        }
    }
}
