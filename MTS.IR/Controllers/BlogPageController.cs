using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Pages;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Routing;

namespace MTS.IR.Controllers
{
    public class BlogPageController : ContentControllerBase<BlogPage>
    {
        public BlogPageController(BlogPageViewModelBuilder blogPageViewModelBuilder)
        {
            BlogPageViewModelBuilder = blogPageViewModelBuilder;
        }

        public BlogPageViewModelBuilder BlogPageViewModelBuilder { get; }

        public IActionResult Index()
        {
            return View(BlogPageViewModelBuilder.BuildList(CurrentItem));
        }

        public IActionResult Details(int id)
        {
            return View(BlogPageViewModelBuilder.BuildDetails(CurrentItem, id));
        }
    }
}
