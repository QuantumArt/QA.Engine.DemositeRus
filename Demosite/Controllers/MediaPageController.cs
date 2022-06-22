using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using Demosite.ViewModels.Builders;

namespace Demosite.Controllers
{
    public class MediaPageController : ContentControllerBase<MediaPage>
    {
        private MediaPageViewModelBuilder modelBuilder { get; }
        public MediaPageController(MediaPageViewModelBuilder builder)
        {
            this.modelBuilder = builder;
        }
        public IActionResult Index()
        {
            var vm = modelBuilder.BuildList(CurrentItem);
            return View(vm);
        }
    }
}
