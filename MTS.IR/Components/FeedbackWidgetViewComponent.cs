using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Widgets;
using MTS.IR.ViewModels;
using QA.DotNetCore.Engine.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class FeedbackWidgetViewComponent : WidgetComponentBase<FeedbackWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(FeedbackWidget widget, IDictionary<string, object> arguments)
        {
            return Task.FromResult<IViewComponentResult>(View(widget));
        }
    }
}
