using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using System.Linq;
using System.Collections.Generic;
using QA.DotNetCore.Engine.QpData;

namespace MTS.IR.ViewModels.Builders
{
    public class ReportBoxViewModelBuilder
    {
        private IAnnualReportsService reportsService { get; }
        public ReportBoxViewModelBuilder(IAnnualReportsService service)
        {
            this.reportsService = service;
        }

        public ReportBoxViewModel BuildList(AbstractWidget widget, IEnumerable<int> ids)
        {
            var vm = new ReportBoxViewModel() { Title = widget.Title };
            var reports = reportsService.GetReports(ids).Select(Map).ToArray();
            vm.Reports.AddRange(reports);
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
        private ReportFileItem Map(ReportFileDto file)
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
