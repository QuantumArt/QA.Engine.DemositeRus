using Demosite.Models.Pages;
using Microsoft.AspNetCore.Mvc;
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
