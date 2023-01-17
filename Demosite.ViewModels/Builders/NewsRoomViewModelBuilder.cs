using Demosite.Interfaces;
using QA.DotNetCore.Engine.Abstractions;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class NewsRoomViewModelBuilder
    {
        private readonly INewsService _newsService;

        public NewsRoomViewModelBuilder(INewsService newsService)
        {
            _newsService = newsService;
        }

        public NewsRoomViewModel BuildBlocks(IAbstractItem widget)
        {
            NewsRoomViewModel viewModel = new() { Title = widget.Title };
            viewModel.Blocks.AddRange(_newsService.GetCategories().Where(c => c.ShowOnStart).OrderBy(o => o.SortOrder).Select(Map).ToArray());
            return viewModel;
        }

        private NewsRoomBlockViewModel Map(Interfaces.Dto.NewsCategoryDto newsCategory)
        {
            NewsRoomBlockViewModel newsBlock = new()
            {
                Title = newsCategory.AlternativeTitle,
                Url = $"/news_and_events/{newsCategory.Alias}"
            };
            newsBlock.Items.AddRange(_newsService.GetAllPosts(categoryId: newsCategory.Id)
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
                PostDate = newsPost.PostDate,
                Brief = newsPost.Brief,
                Url = $"/news_and_events/{categoryAlias}/details/{newsPost.Id}"
            };
        }
    }
}
