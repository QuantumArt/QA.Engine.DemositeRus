using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Widgets;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class ReportBoxWidgetViewComponent : WidgetComponentBase<ReportBoxWidget>
    {
        private ReportBoxViewModelBuilder _modelBuilder { get; }
        public ReportBoxWidgetViewComponent(ReportBoxViewModelBuilder builder)
        {
            this._modelBuilder = builder;
        }
        public override Task<IViewComponentResult> RenderAsync(ReportBoxWidget currentItem, IDictionary<string, object> arguments)
        {
            var ids = currentItem.ReportsItemIds;
            var vm = _modelBuilder.BuildList(currentItem, ids);
            vm.CountReportsToShow = currentItem.CountReportToShow;
            vm.Title = currentItem.Title;
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
