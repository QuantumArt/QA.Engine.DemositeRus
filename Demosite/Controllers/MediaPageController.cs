using Demosite.Models.Pages;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Routing;

namespace Demosite.Controllers
{
    public class MediaPageController : ContentControllerBase<MediaPage>
    {
        private readonly MediaPageViewModelBuilder _modelBuilder;
        public MediaPageController(MediaPageViewModelBuilder builder)
        {
            _modelBuilder = builder;
        }
        public IActionResult Index()
        {
            MediaPageViewModel viewModel = _modelBuilder.BuildList(CurrentItem);
            return View(viewModel);
        }
    }
}
