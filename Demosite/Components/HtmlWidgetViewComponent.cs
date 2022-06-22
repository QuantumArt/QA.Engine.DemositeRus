using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Widgets;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class HtmlWidgetViewComponent : WidgetComponentBase<HtmlWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(HtmlWidget widget, IDictionary<string, object> argumets)
        {
            return Task.FromResult<IViewComponentResult>(View(widget));
        }
    }
}
