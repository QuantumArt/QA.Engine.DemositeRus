using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class NewsRoomWidgetViewComponent: WidgetComponentBase<NewsRoomWidget>
    {
        private NewsRoomViewModelBuilder ModelBuilder { get; }

        public NewsRoomWidgetViewComponent(NewsRoomViewModelBuilder modelBuilder)
        {
            this.ModelBuilder = modelBuilder;
        }

        public override Task<IViewComponentResult> RenderAsync(NewsRoomWidget widget, IDictionary<string, object> arguments)
        {
            var model = ModelBuilder.BuildBlocks(widget);
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
