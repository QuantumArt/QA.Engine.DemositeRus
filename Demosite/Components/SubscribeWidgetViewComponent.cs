using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class SubscribeWidgetViewComponent : WidgetComponentBase<SubscribeWidget>
    {
        private readonly NewsPageViewModelBuilder _modelBuilder;
        public SubscribeWidgetViewComponent(NewsPageViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public override Task<IViewComponentResult> RenderAsync(SubscribeWidget widget, IDictionary<string, object> arguments)
        {
            List<ViewModels.CategoriesListViewModel> categories = _modelBuilder.BuildCategories();
            return Task.FromResult<IViewComponentResult>(View(categories));
        }
    }
}
