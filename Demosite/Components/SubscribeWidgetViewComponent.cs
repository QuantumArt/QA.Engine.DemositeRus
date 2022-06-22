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
    public class SubscribeWidgetViewComponent : WidgetComponentBase<SubscribeWidget>
    {
        private SubscribeViewModelBuilder ModelBuilder { get; }
        public SubscribeWidgetViewComponent(SubscribeViewModelBuilder modelBuilder)
        {
            this.ModelBuilder = modelBuilder;
        }
        public override Task<IViewComponentResult> RenderAsync(SubscribeWidget widget, IDictionary<string, object> arguments)
        {
            var model = ModelBuilder.BuildForm(widget);
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
