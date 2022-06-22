using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Pages;
using QA.DotNetCore.Engine.Routing;

namespace Demosite.Controllers
{
    public class RedirectPageController : ContentControllerBase<RedirectPage>
    {
        public IActionResult Index()
        {
            return new RedirectResult(CurrentItem.RedirectTo, true);
        }
    }
}
