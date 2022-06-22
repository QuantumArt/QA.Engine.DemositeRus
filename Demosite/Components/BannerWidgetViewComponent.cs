using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class BannerWidgetViewComponent : WidgetComponentBase<BannerWidget>
    {
        private BannerWidgetViewModelBuilder _modelBuilder { get; }
        public BannerWidgetViewComponent(BannerWidgetViewModelBuilder builder)
        {
            this._modelBuilder = builder;
        }
        public override Task<IViewComponentResult> RenderAsync(BannerWidget currentItem, IDictionary<string, object> arguments)
        {
            var vm = _modelBuilder.Build(currentItem.BannerItemIds);
            vm.SwipeDelay = currentItem.SwipeDelay;
            vm.Title = currentItem.Title;
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
