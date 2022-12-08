using Microsoft.AspNetCore.Mvc;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Abstractions;
using System.Threading.Tasks;
using Demosite.Services;
using Demosite.Interfaces;

namespace Demosite.Components
{
    public class NewsListViewComponent : ViewComponent
    {
        private NewsPageViewModelBuilder NewsPageViewModelBuilder { get; }
        private ISiteSettingsService SiteSettingsProvider { get; }
        public NewsListViewComponent(NewsPageViewModelBuilder newsPageViewModelBuilder,
                                     ISiteSettingsService siteSettings)
        {
            this.NewsPageViewModelBuilder = newsPageViewModelBuilder;
            this.SiteSettingsProvider = siteSettings;   
        }

        public async Task<IViewComponentResult> InvokeAsync(IAbstractPage CurrentItem, int? year, int? month, int? categoryId, int? page)
        {
            var itemsOnPage = await SiteSettingsProvider.NewsPaginatedItemsCountAsync();
            var vm = NewsPageViewModelBuilder.BuildList(CurrentItem, year, month, categoryId, page ?? 1, count: itemsOnPage);
            return await Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
