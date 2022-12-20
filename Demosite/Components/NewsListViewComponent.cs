using Demosite.Interfaces;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Abstractions;
using System.Threading.Tasks;

namespace Demosite.Components
{
    public class NewsListViewComponent : ViewComponent
    {
        private readonly NewsPageViewModelBuilder _newsPageViewModelBuilder;
        private readonly ISiteSettingsService _siteSettingsProvider;
        public NewsListViewComponent(NewsPageViewModelBuilder newsPageViewModelBuilder,
                                     ISiteSettingsService siteSettings)
        {
            _newsPageViewModelBuilder = newsPageViewModelBuilder;
            _siteSettingsProvider = siteSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync(IAbstractPage CurrentItem, int? year, int? month, int? categoryId, int? page)
        {
            int itemsOnPage = await _siteSettingsProvider.NewsPaginatedItemsCountAsync();
            ViewModels.NewsPageViewModel viewModel = _newsPageViewModelBuilder.BuildList(CurrentItem, year, month, categoryId, page ?? 1, count: itemsOnPage);
            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
