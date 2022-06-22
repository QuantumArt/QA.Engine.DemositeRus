using MTS.IR.Interfaces;
using QA.DotNetCore.Engine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using MTS.IR.ViewModels.Helpers;

namespace MTS.IR.ViewModels.Builders
{
    public class NewsPageViewModelBuilder
    {
        private INewsService NewsService { get; }

        public NewsPageViewModelBuilder(INewsService newsService)
        {
            this.NewsService = newsService;
        }

        public NewsPageViewModel BuildList(IAbstractPage newsPage, int? year = null, int? month = null, int? categoryId = null, int page = 1, int count = 10)
        {
            var rand = new Random();
            var vm = new NewsPageViewModel { Header = newsPage.Title };
            var news = NewsService.GetAllPosts(year, month, categoryId);
            vm.Items.AddRange(news.Skip((page - 1) * count)
                                  .Take(count)
                                  .Select(p => new NewsItemInListViewModel
                                  {
                                      Id = p.Id,
                                      Title = p.Title,
                                      Date = p.PostDate,
                                      Brief = p.Brief,
                                      CategoryName = p.Category.Title,
                                      Url = $"{newsPage.GetUrl()}/details/{p.Id}"
                                  }));
            var newsCount = news.Count();
            vm.PageCount = newsCount / count;
            if(newsCount % count > 0)
            {
                vm.PageCount++;
            }
            vm.CurrentPage = page;
            vm.BreadCrumbs = newsPage.GetBreadCrumbs();
            return vm;
        }

        public NewsDetailsViewModel BuildDetails(IAbstractPage newsPage, int id, string commonText)
        {
            var post = NewsService.GetPost(id);
            var breadCrumbs = newsPage.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbViewModel()
            {
                Text = "Detail news page"
            });
            return new NewsDetailsViewModel
            {
                Title = post.Title,
                Date = post.PostDate,
                Text = post.Text,
                CategoryName = post.Category.Title,
                AllnewsUrl = newsPage.GetUrl(),
                CommonText = commonText,
                BreadCrumbs = breadCrumbs,
            };
        }

        public List<CategoriesListViewModel> BuildCategories()
        {
            return NewsService.GetCategories().Select(x => new CategoriesListViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
        }
    }
}
