using Demosite.Components;
using Demosite.Interfaces;
using Demosite.Models.Pages;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Routing;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    public class NewsPageController : ContentControllerBase<NewsPage>
    {
        private readonly NewsPageViewModelBuilder _newsPageViewModelBuilder;
        private readonly ISiteSettingsService _siteSettingsProvider;
        public NewsPageController(NewsPageViewModelBuilder newsPageViewModelBuilder,
                                  ISiteSettingsService siteSettings)
        {
            _newsPageViewModelBuilder = newsPageViewModelBuilder;
            _siteSettingsProvider = siteSettings;
        }

        public async Task<IActionResult> Index()
        {
            int? categoryId = CurrentItem.CategoryId;
            int itemsOnPage = await _siteSettingsProvider.NewsPaginatedItemsCountAsync();
            NewsPageViewModel viewModel = _newsPageViewModelBuilder.BuildList(CurrentItem, categoryId: categoryId, count: itemsOnPage);
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            NewsDetailsViewModel viewModel = _newsPageViewModelBuilder.BuildDetails(CurrentItem, id, CurrentItem.DetailsText, CurrentItem.CategoryId);
            return viewModel == null ? NotFound() : View(viewModel);
        }

        public IActionResult Get(int? year, int? month, int? page)
        {
            int? categoryId = CurrentItem.CategoryId;
            return ViewComponent(typeof(NewsListViewComponent), new { CurrentItem, year, month, categoryId, page });
        }
    }
}
