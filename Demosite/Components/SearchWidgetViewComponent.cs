using Demosite.Models.Widgets;
using Demosite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class SearchWidgetViewComponent : WidgetComponentBase<SearchWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(SearchWidget widget, IDictionary<string, object> arguments)
        {
            var seacrh = new SearchBarViewModel("");
            return Task.FromResult<IViewComponentResult>(View(seacrh));
        }
    }
}
