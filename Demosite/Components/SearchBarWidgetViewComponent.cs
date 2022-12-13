using Demosite.Models.Widgets;
using Demosite.ViewModels.SearchBar;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class SearchBarWidgetViewComponent : WidgetComponentBase<SearchBarWidget>
    {
        public override Task<IViewComponentResult> RenderAsync(SearchBarWidget widget, IDictionary<string, object> arguments)
        {
            SearchBarViewModel seacrh = new();
            return Task.FromResult<IViewComponentResult>(View(seacrh));
        }
    }
}
