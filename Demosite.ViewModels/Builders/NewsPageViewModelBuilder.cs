using Demosite.Interfaces;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class NewsPageViewModelBuilder
    {
        private readonly INewsService _newsService;

        public NewsPageViewModelBuilder(INewsService newsService)
        {
            _newsService = newsService;
        }

        public NewsPageViewModel BuildList(IAbstractPage newsPage, int? year = null, int? month = null, int? categoryId = null, int pageNumber = 1, int count = 10)
        {
            NewsPageViewModel viewModel = new() { Header = newsPage.Title };
            IEnumerable<Interfaces.Dto.NewsPostDto> news = _newsService.GetAllPosts(year, month, categoryId);
            viewModel.DateDictionary = _newsService.GetPostsDateDictionary(categoryId);
            viewModel.Items.AddRange(news.Skip((pageNumber - 1) * count)
                                  .Take(count)
                                  .Select(p => new NewsItemInListViewModel
                                  {
                                      Id = p.Id,
                                      Title = p.Title,
                                      Date = p.PostDate,
                                      Brief = p.Brief,
                                      CategoryName = p.Category.Title,
                                      Published = p.Published,
                                      Url = $"{newsPage.GetUrl()}/details/{p.Id}"
                                  }));
            viewModel.PageCount = (news.Count() + count - 1) / count;
            viewModel.CurrentPage = pageNumber;
            viewModel.BreadCrumbs = newsPage.GetBreadCrumbs();
            return viewModel;
        }

        public NewsDetailsViewModel BuildDetails(IAbstractPage newsPage, int id, string commonText, int? categoryId)
        {
            Interfaces.Dto.NewsPostDto post = _newsService.GetPost(id, categoryId);
            if (post == null)
            {
                return null;
            }
            List<BreadCrumbViewModel> breadCrumbs = newsPage.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbViewModel()
            {
                Text = "Детальная информация"
            });
            return new NewsDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Date = post.PostDate,
                Text = post.Text,
                Published = post.Published,
                CategoryName = post.Category.Title,
                AllnewsUrl = newsPage.GetUrl(),
                CommonText = commonText,
                BreadCrumbs = breadCrumbs,
            };
        }

        public List<CategoriesListViewModel> BuildCategories()
        {
            return _newsService.GetCategories().Select(x => new CategoriesListViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
        }
    }
}
