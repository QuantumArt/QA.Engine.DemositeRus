using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Widgets;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class HtmlWidgetViewComponent : WidgetComponentBase<HtmlWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(HtmlWidget widget, IDictionary<string, object> argumets)
        {
            return Task.FromResult<IViewComponentResult>(View(widget));
        }
    }
}
