using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Collections;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class AnnualReportsPageViewModelBuilder
    {
        private IAnnualReportsService reportsService { get; }
        public AnnualReportsPageViewModelBuilder(IAnnualReportsService service)
        {
            this.reportsService = service;
        }
        public AnnualReportsPageViewModel BuildForm(IAbstractPage page, int? id)
        {
            var vm = new AnnualReportsPageViewModel() { Title = page.Title };
            vm.Report = Map(reportsService.GetReport(id));
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
