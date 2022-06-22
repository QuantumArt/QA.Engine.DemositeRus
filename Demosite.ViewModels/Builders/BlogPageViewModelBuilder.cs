using Demosite.Interfaces;
using QA.DotNetCore.Engine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class BlogPageViewModelBuilder
    {
        public IBlogService BlogService { get; }

        public BlogPageViewModelBuilder(IBlogService blogService)
        {
            BlogService = blogService;
        }

        public BlogPageViewModel BuildList(IAbstractPage blogPage)
        {
            var rnd = new Random();
            var vm = new BlogPageViewModel { Header = blogPage.Title };
            vm.Items.AddRange(BlogService.GetAllPosts().Select(p => new BlogItemInListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Brief = p.Brief,
                Date = p.PostDate,
                CategoryName = p.Category?.Title,
                Image = p.Image,
                Url = blogPage.GetUrl() + "/details/" + p.Id,
                Published = p.Published,
                Comments = rnd.Next(20)
            }));
            return vm;
        }

        public BlogDetailsViewModel BuildDetails(IAbstractPage blogPage, int id)
        {
            var dto = BlogService.GetPost(id);

            return new BlogDetailsViewModel
            {
                Title = dto.Title,
                Date = dto.PostDate,
                CategoryName = dto.Category?.Title,
                Image = dto.Image,
                Tags = dto.Tags.Select(t => t.Title).ToList(),
                Text = dto.Text,
            };
        }

        public List<string> BuildCategories()
        {
            return BlogService.GetCategories().Select(x => x.Title).ToList();
        }

        public List<string> BuildTags()
        {
            return BlogService.GetTags().Select(x => x.Title).ToList();
        }
    }
}
