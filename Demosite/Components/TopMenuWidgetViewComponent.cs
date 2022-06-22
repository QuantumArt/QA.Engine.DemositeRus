using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.QpData;
using QA.DotNetCore.Engine.Routing;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class TopMenuWidgetViewComponent : WidgetComponentBase<TopMenuWidget>
    {
        public TopMenuWidgetViewComponent(MenuViewModelBuilder menuViewModelBuilder)
        {
            MenuViewModelBuilder = menuViewModelBuilder;
        }

        public MenuViewModelBuilder MenuViewModelBuilder { get; }

        public override Task<IViewComponentResult> RenderAsync(TopMenuWidget widget, IDictionary<string, object> arguments)
        {
            var vm = MenuViewModelBuilder.Build(ViewContext.GetStartPage(), ViewContext.GetCurrentItem<AbstractPage>());
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
