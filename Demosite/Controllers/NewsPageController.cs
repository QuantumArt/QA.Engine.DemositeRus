using Microsoft.AspNetCore.Mvc;
using Demosite.Components;
using Demosite.Helpers;
using Demosite.Models.Pages;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Routing;
using System;

namespace Demosite.Controllers
{
    public class NewsPageController : ContentControllerBase<NewsPage>
    {
        private NewsPageViewModelBuilder NewsPageViewModelBuilder { get; }
        public NewsPageController(NewsPageViewModelBuilder newsPageViewModelBuilder)
        {
            this.NewsPageViewModelBuilder = newsPageViewModelBuilder;
        }
        public IActionResult Index()
        {
            var categoryId = CurrentItem.CategoryId;
            var year = DateTime.Now.Year;
            var vm = NewsPageViewModelBuilder.BuildList(CurrentItem, year: year, categoryId: categoryId);
            return View(vm);
        }


        public IActionResult Details(int id)
        {
            var vm = NewsPageViewModelBuilder.BuildDetails(CurrentItem, id, CurrentItem.DetailsText);
            return View(vm);
        }

        public IActionResult Get(int? year, int? month, int? page)
        {
            var categoryId = CurrentItem.CategoryId;
            return ViewComponent(typeof(NewsListViewComponent), new { CurrentItem, year, month, categoryId, page });
        }
    }
}
