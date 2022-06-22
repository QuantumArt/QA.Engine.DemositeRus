using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Widgets;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
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
