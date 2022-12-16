using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class NewsRoomWidgetViewComponent : WidgetComponentBase<NewsRoomWidget>
    {
        private readonly NewsRoomViewModelBuilder _modelBuilder;

        public NewsRoomWidgetViewComponent(NewsRoomViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public override Task<IViewComponentResult> RenderAsync(NewsRoomWidget widget, IDictionary<string, object> arguments)
        {
            ViewModels.NewsRoomViewModel model = _modelBuilder.BuildBlocks(widget);
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
