using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class BannerWidgetViewComponent : WidgetComponentBase<BannerWidget>
    {
        private readonly BannerWidgetViewModelBuilder _modelBuilder;
        public BannerWidgetViewComponent(BannerWidgetViewModelBuilder builder)
        {
            _modelBuilder = builder;
        }
        public override Task<IViewComponentResult> RenderAsync(BannerWidget currentItem, IDictionary<string, object> arguments)
        {
            ViewModels.BannerListViewModel viewModel = _modelBuilder.Build(currentItem.BannerItemIds);
            viewModel.SwipeDelay = currentItem.SwipeDelay;
            viewModel.Title = currentItem.Title;
            return Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
