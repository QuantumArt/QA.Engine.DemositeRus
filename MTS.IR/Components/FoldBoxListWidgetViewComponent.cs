using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Widgets;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class FoldBoxListWidgetViewComponent : WidgetComponentBase<FoldBoxListWidget>
    {
        private FoldBoxListViewModelBuilder ModelBuilder { get; }

        public FoldBoxListWidgetViewComponent(FoldBoxListViewModelBuilder modelBuilder)
        {
            this.ModelBuilder = modelBuilder;
        }
        public override Task<IViewComponentResult> RenderAsync(FoldBoxListWidget widget, IDictionary<string, object> argumets)
        {

            var vm = this.ModelBuilder.Build(widget.FoldBoxListItemIds, widget.WidgetType);
            vm.Header = widget.Title;
            vm.SlidesToShow = widget.SlidesToShow;
            return Task.FromResult<IViewComponentResult>(View(vm.WidgetType, vm));
        }
    }
}
