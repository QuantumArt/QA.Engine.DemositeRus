using Microsoft.EntityFrameworkCore;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using QA.DotNetCore.Engine.Persistent.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class BlogService : IBlogService
    {
        public BlogService(QpSettings qpSettings, IDbContext context)
        {
            QpDataContext = context;
        }

        public IDbContext QpDataContext { get; }

        public IEnumerable<BlogPostDto> GetAllPosts()
        {
            return (QpDataContext as PostgreQpDataContext).BlogPosts
                   .Include(c => c.Category)
                   .Include(t => t.Tags)
                   .ThenInclude(ti => ti.BackwardForTags_BlogPost).ToList()
                   .Select(Map).ToArray();
        }

        public BlogPostDto GetPost(int id)
        {
            return Map((QpDataContext as PostgreQpDataContext).BlogPosts
                 .Include(c => c.Category)
                 .Include(t => t.Tags)
                 .ThenInclude(ti => ti.BackwardForTags_BlogPost)
                 .FirstOrDefault(bp => bp.Id == id));
        }

        public IEnumerable<BlogTagDto> GetTags()
        {
            return (QpDataContext as PostgreQpDataContext).BlogTags
                  .Select(Map).ToArray();
        }
        public IEnumerable<BlogCategoryDto> GetCategories()
        {
            return (QpDataContext as PostgreQpDataContext).BlogCategories
                  .Select(Map).ToArray();
        }


       

        private BlogPostDto Map(Postgre.DAL.BlogPost blogPost)
        {
            if (blogPost == null)
                return null;

            return new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Brief = blogPost.Brief,
                Category = Map(blogPost.Category),
                Image = blogPost.ImageUrl,
                PostDate = blogPost.PostDate.GetValueOrDefault(new DateTime(2001, 01, 01)),
                Text = blogPost.Text,
                Tags = blogPost.Tags.Select(Map).ToList(),
                Published = blogPost.StatusTypeId == 143
            };
        }
        

        private BlogCategoryDto Map(Postgre.DAL.BlogCategory blogCategory)
        {
            if (blogCategory == null)
                return null;

            return new BlogCategoryDto
            {
                Id = blogCategory.Id,
                Title = blogCategory.Title,
                SortOrder = blogCategory.SortOrder
            };
        }
        

        private BlogTagDto Map(Postgre.DAL.BlogPost2BlogTagForTags blogTag)
        {
            if (blogTag == null || blogTag.BlogTagLinkedItem == null)
                return null;

            return new BlogTagDto
            {
                Id = blogTag.BlogTagLinkedItem.Id,
                Title = blogTag.BlogTagLinkedItem.Title
            };
        }
        private BlogTagDto Map(Postgre.DAL.BlogTag blogTag)
        {
            if (blogTag == null)
                return null;

            return new BlogTagDto
            {
                Id = blogTag.Id,
                Title = blogTag.Title
            };
        }
    }
}
