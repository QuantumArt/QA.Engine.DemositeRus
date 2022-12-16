using Demosite.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using QA.DotNetCore.Engine.Abstractions;
using QA.DotNetCore.Engine.Abstractions.Targeting;
using QA.DotNetCore.Engine.QpData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class MenuViewModelBuilder
    {
        private const int MENU_DEPTH = 3;
        private readonly ITargetingUrlTransformator _urlTransformator;
        private ICacheService _memoryCache { get; set; }
        private readonly IServiceProvider _serviceProvider;
        public MenuViewModelBuilder(ITargetingUrlTransformator urlTransformator,
                                    IServiceProvider serviceProvider)
        {
            _urlTransformator = urlTransformator;
            _serviceProvider = serviceProvider;
        }

        public MenuViewModel Build(IStartPage startPage, AbstractPage currentPage)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            _memoryCache = scope.ServiceProvider.GetService<ICacheService>();
            return _memoryCache.GetFromCache<MenuViewModel>(GetCacheKey(startPage.Id), () =>
            {
                if (startPage == null) return null;

                MenuViewModel model = new();
                IOrderedEnumerable<AbstractPage> topLevelItems = startPage.GetChildren()
                    .Where(w => w.IsPage)
                    .OfType<AbstractPage>()
                    .Where(p => p.IsVisible)
                    .OrderBy(o => o.SortOrder);

                int currentPageId = (currentPage?.Id).GetValueOrDefault(0);//currentPage может быть Null, если страница не в структуре сайта

                foreach (AbstractPage tlitem in topLevelItems)
                {
                    List<MenuItem> resultBuildMenu = BuildMenu(tlitem, MENU_DEPTH, currentPageId);
                    model.Items.Add(new MenuItem
                    {
                        Id = tlitem.Id,
                        Title = tlitem.Title,
                        Alias = tlitem.Alias,
                        Href = tlitem.GetUrl(_urlTransformator),
                        Children = resultBuildMenu,
                        IsActive = tlitem.Id == currentPageId,
                        HasActiveChild = resultBuildMenu.Where(w => w.IsActive).Any()
                    });
                }

                model.Items = model.Items?.OrderBy(o => o.Order).ToList();

                return model;
            });
        }

        private List<MenuItem> BuildMenu(AbstractPage item, int level, int currentId)
        {
            if (level <= 0)
            {
                return null;
            }

            List<MenuItem> itemList = new();
            foreach (AbstractPage itemlv in item.GetChildren().Where(w => w.IsPage).OfType<AbstractPage>().Where(p => p.IsVisible).OrderBy(o => o.SortOrder))
            {
                List<MenuItem> resultBuidMenu = BuildMenu(itemlv, level - 1, currentId);
                itemList.Add(new MenuItem
                {
                    Title = itemlv.Title,
                    Alias = itemlv.Alias,
                    Href = itemlv.GetUrl(_urlTransformator),
                    Children = resultBuidMenu,
                    IsActive = itemlv.Id == currentId || resultBuidMenu.Where(w => w.IsActive).Any()
                });
            }
            return itemList;
        }

        private static string GetCacheKey(int id)
        {
            return $"menu_item_{id}";
        }
    }
}
