using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.QpData;
using QA.DotNetCore.Engine.Routing;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class TopMenuWidgetViewComponent : WidgetComponentBase<TopMenuWidget>
    {
        private readonly MenuViewModelBuilder _menuViewModelBuilder;
        public TopMenuWidgetViewComponent(MenuViewModelBuilder menuViewModelBuilder)
        {
            _menuViewModelBuilder = menuViewModelBuilder;
        }

        public override Task<IViewComponentResult> RenderAsync(TopMenuWidget widget, IDictionary<string, object> arguments)
        {
            ViewModels.MenuViewModel viewModel = _menuViewModelBuilder.Build(ViewContext.GetStartPage(), ViewContext.GetCurrentItem<AbstractPage>());
            return Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
