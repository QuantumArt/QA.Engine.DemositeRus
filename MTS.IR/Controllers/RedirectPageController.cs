using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Pages;
using QA.DotNetCore.Engine.Routing;

namespace MTS.IR.Controllers
{
    public class RedirectPageController : ContentControllerBase<RedirectPage>
    {
        public IActionResult Index()
        {
            return new RedirectResult(CurrentItem.RedirectTo, true);
        }
    }
}
