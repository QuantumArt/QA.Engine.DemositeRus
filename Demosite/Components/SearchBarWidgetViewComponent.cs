using Demosite.Models.Widgets;
using Demosite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class SearchBarWidgetViewComponent : WidgetComponentBase<SearchBarWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(SearchBarWidget widget, IDictionary<string, object> arguments)
        {
            var seacrh = new SearchBarViewModel(null);
            return Task.FromResult<IViewComponentResult>(View(seacrh));
        }
    }
}
