using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using Demosite.Postgre.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class NewsService : INewsService
    {
        private readonly IDbContext _qpDataContext;
        private readonly ICacheService _memoryCache;
        private readonly CacheTagUtilities _cacheTagUtilities;
        public NewsService(IDbContext context, ICacheService memoryCache, CacheTagUtilities cacheTagUtilities)
        {
            _qpDataContext = context;
            _memoryCache = memoryCache;
            _cacheTagUtilities = cacheTagUtilities;
        }

        public IEnumerable<NewsPostDto> GetAllPosts(int? year = null, int? month = null, int? categoryId = null)
        {
            var cacheTags = _cacheTagUtilities.Merge(CacheTags.NewsPost);
            return _memoryCache.GetFromCache<IEnumerable<NewsPostDto>>(GetCacheKey(year, month, categoryId), cacheTags, () =>
               {
                   IQueryable<NewsPost> query = (_qpDataContext as PostgreQpDataContext).NewsPosts.AsNoTracking();

                   if (year.HasValue)
                   {
                       query = query.Where(q => q.PostDate.HasValue && q.PostDate.Value.Year == year);
                   }

                   if (month.HasValue)
                   {
                       query = query.Where(q => q.PostDate.HasValue && q.PostDate.Value.Month == month);
                   }

                   if (categoryId.HasValue)
                   {
                       query = query.Where(q => q.Category_ID.HasValue && q.Category_ID == categoryId);
                   }
                   return query.Include(n => n.Category)
                               .OrderByDescending(o => o.PostDate)
                               .Select(Map)
                               .ToArray();
               });
        }

        public IEnumerable<NewsCategoryDto> GetCategories()
        {
            return (_qpDataContext as PostgreQpDataContext).NewsCategories
                 .Select(Map).ToArray();
        }

        public NewsPostDto GetPost(int id, int? categoryId)
        {
            var cacheTags = _cacheTagUtilities.Merge(CacheTags.NewsPost);
            return _memoryCache.GetFromCache<NewsPostDto>(GetCacheKey(id, categoryId), cacheTags, () =>
            {
                IQueryable<NewsPost> query = (_qpDataContext as PostgreQpDataContext).NewsPosts.AsNoTracking();
                query = categoryId.HasValue ? query.Where(bp => bp.Id == id && bp.Category.Id == categoryId.Value) : query.Where(bp => bp.Id == id);
                query = query.Include(c => c.Category);
                return Map(query.FirstOrDefault());
            });
        }

        public NewsPostDto[] GetPosts(int[] ids)
        {
            return (_qpDataContext as PostgreQpDataContext).NewsPosts
                .Include(c => c.Category)
                .Where(p => ids.Contains(p.Id))
                .Select(Map)
                .ToArray();
        }

        private NewsPostDto Map(Postgre.DAL.NewsPost post)
        {
            return post == null
                ? null
                : new NewsPostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    PostDate = post.PostDate.GetValueOrDefault(new DateTime(2001, 01, 01)),
                    Brief = post.Brief,
                    Text = post.Text,
                    Category = Map(post.Category),
                    Published = post.StatusTypeId == 143
                };
        }

        private NewsCategoryDto Map(Postgre.DAL.NewsCategory category)
        {
            return category == null
                ? null
                : new NewsCategoryDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    AlternativeTitle = category.AlternativeTitle,
                    Alias = category.Alias,
                    ShowOnStart = category.ShowOnStart ?? false,
                    SortOrder = category.SortOrder
                };
        }

        public IEnumerable<NewsPostDto> GetAllPosts(PostRequest request, int[] categoryIds = null)
        {
            IQueryable<NewsPost> query = (_qpDataContext as PostgreQpDataContext).NewsPosts.AsNoTracking();
            if (request.FromDate.HasValue)
            {
                query = query.Where(n => n.PostDate >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                query = query.Where(n => n.PostDate <= request.ToDate.Value);
            }

            if (categoryIds != null && categoryIds.Length > 0)
            {
                query = query.Where(q => q.Category_ID.HasValue && categoryIds.Contains(q.Category_ID.Value));
            }
            return query.Include(n => n.Category)
                        .OrderByDescending(o => o.PostDate)
                        .Select(Map)
                        .ToArray();
        }

        public Dictionary<int, int[]> GetPostsDateDictionary(int? categoryId)
        {
            var cacheTags = _cacheTagUtilities.Merge(CacheTags.NewsPost);
            return _memoryCache.GetFromCache<Dictionary<int, int[]>>(GetCacheKeyPostDate(categoryId), cacheTags, () =>
            {
                var query = (_qpDataContext as PostgreQpDataContext).NewsPosts.AsNoTracking()
                    .Where(news => news.PostDate.HasValue);
                if(categoryId.HasValue)
                {
                    query = query.Where(news => news.Category_ID.Value == categoryId.Value);
                }
                var queryFromDB = query.Select(news => new { year = news.PostDate.Value.Year, month = news.PostDate.Value.Month })
                    .Distinct()
                    .AsEnumerable();
                Dictionary<int, int[]> result = queryFromDB.GroupBy(keySelector => keySelector.year, val => val.month)
                    .ToDictionary(key => key.Key, value => value.ToArray());
                return result;
            });
        }

        private static string GetCacheKey(int id, int? categoryId = null)
        {
            return $"news_post_{id}" + (categoryId.HasValue ? $"_category_{categoryId}" : $"_withoutcategory");
        }

        private static string GetCacheKey(int? year = null, int? month = null, int? categoryId = null)
        {
            return $"news_post_all_{(year.HasValue ? year.Value : string.Empty)}_{(month.HasValue ? month.Value : string.Empty)}_{(categoryId.HasValue ? categoryId.Value : string.Empty)}";
        }

        private static string GetCacheKeyPostDate(int? categoryId = null)
        {
            return nameof(GetPostsDateDictionary) + (categoryId.HasValue ? $"_category_{categoryId}" : $"_withoutcategory");
        }
    }
}
