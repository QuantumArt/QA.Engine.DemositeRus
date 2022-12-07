using Demosite.Components;
using Demosite.Interfaces;
using Demosite.Models;
using Demosite.Models.Pages;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    public class NewsPageController : ContentControllerBase<NewsPage>
    {
        private NewsPageViewModelBuilder NewsPageViewModelBuilder { get; }
        private ISiteSettingsProvider SiteSettingsProvider { get; }
        public NewsPageController(NewsPageViewModelBuilder newsPageViewModelBuilder,
                                  ISiteSettingsProvider siteSettings)
        {
            this.NewsPageViewModelBuilder = newsPageViewModelBuilder;
            this.SiteSettingsProvider = siteSettings;
        }
        public async Task<IActionResult> Index()
        {
            var categoryId = CurrentItem.CategoryId;
            var year = DateTime.Now.Year;
            var itemsOnPage = await SiteSettingsProvider.NewsPaginatedItemsCountAsync();
            var vm = NewsPageViewModelBuilder.BuildList(CurrentItem, year: year, categoryId: categoryId, count: itemsOnPage);
            return View(vm);
        }


        public IActionResult Details(int id)
        {
            var vm = NewsPageViewModelBuilder.BuildDetails(CurrentItem, id, CurrentItem.DetailsText, CurrentItem.CategoryId);
            if (vm == null)
            {
                return NotFound();
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
