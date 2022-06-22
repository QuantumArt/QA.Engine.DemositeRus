using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using Demosite.ViewModels;
using QA.DotNetCore.Engine.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class FeedbackWidgetViewComponent : WidgetComponentBase<FeedbackWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(FeedbackWidget widget, IDictionary<string, object> arguments)
        {
            return Task.FromResult<IViewComponentResult>(View(widget));
        }
    }
}
