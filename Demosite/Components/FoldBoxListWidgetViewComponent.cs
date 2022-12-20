using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class FoldBoxListWidgetViewComponent : WidgetComponentBase<FoldBoxListWidget>
    {
        private readonly FoldBoxListViewModelBuilder _modelBuilder;

        public FoldBoxListWidgetViewComponent(FoldBoxListViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public override Task<IViewComponentResult> RenderAsync(FoldBoxListWidget widget, IDictionary<string, object> argumets)
        {

            ViewModels.FoldBoxListViewModel viewModel = _modelBuilder.Build(widget.FoldBoxListItemIds, widget.WidgetType);
            viewModel.Header = widget.Title;
            viewModel.SlidesToShow = widget.SlidesToShow;
            return Task.FromResult<IViewComponentResult>(View(viewModel.WidgetType, viewModel));
        }
    }
}
