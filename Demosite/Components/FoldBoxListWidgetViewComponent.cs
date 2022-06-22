using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
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
