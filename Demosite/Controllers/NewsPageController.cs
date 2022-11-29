using Demosite.Components;
using Demosite.Models;
using Demosite.Models.Pages;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
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
            var vm = NewsPageViewModelBuilder.BuildDetails(CurrentItem, id, CurrentItem.DetailsText, CurrentItem.CategoryId);
            if(vm == null)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "Запрашиваемвая Вами страница не найдена, пожалуйста проверьте корректность запрашиваемых данных"
                };
                return View("Error", error);
            }
            return View(vm);
        }

        public IActionResult Get(int? year, int? month, int? page)
        {
            var categoryId = CurrentItem.CategoryId;
            return ViewComponent(typeof(NewsListViewComponent), new { CurrentItem, year, month, categoryId, page });
        }
    }
}
