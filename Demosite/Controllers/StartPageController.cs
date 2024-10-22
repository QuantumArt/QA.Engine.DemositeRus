using Demosite.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Abstractions;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QA.DotNetCore.Engine.QpData;

namespace Demosite.Controllers
{
    public class StartPageController : ContentControllerBase<StartPage>
    {
        public IActionResult Index()
        {
            IAbstractItem firstChildPage = CurrentItem.GetChildren().OfType<AbstractPage>().OrderBy(i => i.SortOrder).FirstOrDefault();
            return firstChildPage != null ? (IActionResult)new RedirectResult(firstChildPage.GetUrl(), false) : throw new Exception("Site is empty");
        }
    }
}
