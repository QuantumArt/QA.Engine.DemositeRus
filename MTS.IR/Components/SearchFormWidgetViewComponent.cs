using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.QpData;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class SearchFormWidgetViewComponent : WidgetComponentBase<AbstractWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(AbstractWidget widget, IDictionary<string, object> argumets)
        {
            return Task.FromResult<IViewComponentResult>(View(widget));
        }
    }
}
