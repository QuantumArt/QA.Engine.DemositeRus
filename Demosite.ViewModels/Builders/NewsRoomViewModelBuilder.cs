using Demosite.Interfaces;
using QA.DotNetCore.Engine.Abstractions;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class NewsRoomViewModelBuilder
    {
        private INewsService NewsService { get; }

        public NewsRoomViewModelBuilder(INewsService newsService)
        {
            this.NewsService = newsService;
        }

        public NewsRoomViewModel BuildBlocks(IAbstractItem widget)
        {
            var vm = new NewsRoomViewModel { Title = widget.Title };
            vm.Blocks.AddRange(NewsService.GetCategories().Where(c => c.ShowOnStart).OrderBy(o => o.SortOrder).Select(Map).ToArray());
            return vm;
        }

        private NewsRoomBlockViewModel Map(Interfaces.Dto.NewsCategoryDto newsCategory)
        {
            var newsBlock = new NewsRoomBlockViewModel
            {
                Title = newsCategory.AlternativeTitle,
                Url = $"/news_and_events/{newsCategory.Alias}"
            };
            newsBlock.Items.AddRange(NewsService.GetAllPosts(categoryId: newsCategory.Id)
                                                .Take(3)
                                                .Select(p => Map(p, newsCategory.Alias))
                                                .ToArray());
            return newsBlock;
        }

        private NewsRoomBlockItemViewModel Map(Interfaces.Dto.NewsPostDto newsPost, string categoryAlias)
        {
            return new NewsRoomBlockItemViewModel
            {
                Title = newsPost.Title,
                PostData = newsPost.PostDate,
                Brief = newsPost.Brief,
                Url = $"/news_and_events/{categoryAlias}/details/{newsPost.Id}"
            };
        }
    }
}
