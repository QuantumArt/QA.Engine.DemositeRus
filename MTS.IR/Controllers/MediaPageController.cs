using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using MTS.IR.ViewModels.Builders;

namespace MTS.IR.Controllers
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
