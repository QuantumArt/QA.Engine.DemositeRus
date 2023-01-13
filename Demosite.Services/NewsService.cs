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

        public IEnumerable<NewsPostDto> GetAllPosts(PostRequest request)
        {
            IQueryable<NewsPost> query = (_qpDataContext as PostgreQpDataContext).NewsPosts.AsNoTracking();
            if (request.IsPublished.HasValue)
            {
                int publishedId = (_qpDataContext as PostgreQpDataContext).PublishedId;
                if (request.IsPublished.Value)
                {
                    query = query.Where(n => n.StatusTypeId == publishedId);
                }
                else
                {
                    query = query.Where(n => n.StatusTypeId != publishedId);
                }
            }
            if (request.FromDate.HasValue)
            {
                DateTimeOffset fromDate = request.FromDate.Value.ToDateTime(new TimeOnly(), DateTimeKind.Local);
                query = query.Where(n => n.PostDate >= fromDate.UtcDateTime);
            }
            if (request.ToDate.HasValue)
            {
                DateTimeOffset toDate = request.FromDate.Value.ToDateTime(new TimeOnly(), DateTimeKind.Local);
                query = query.Where(n => n.PostDate <= toDate.UtcDateTime);
            }
            if (request.CategoryIds != null && request.CategoryIds.Values.Any())
            {
                if (request.CategoryIds.Inverted)
                {
                    query = query.Where(q => q.Category_ID.HasValue && !request.CategoryIds.Values.Contains(q.Category_ID.Value));
                }
                else
                {
                    query = query.Where(q => q.Category_ID.HasValue && request.CategoryIds.Values.Contains(q.Category_ID.Value));
                }
            }
            if (request.NewsIds != null && request.NewsIds.Values.Any())
            {
                if (request.NewsIds.Inverted)
                {
                    query = query.Where(n => !request.NewsIds.Values.Contains(n.Id));
                }
                else
                {
                    query = query.Where(n => request.NewsIds.Values.Contains(n.Id));
                }
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
                    .Where(np => np.PostDate.HasValue && (categoryId == null || np.Category_ID == categoryId))
                    .Select(np => new { np.PostDate.Value.Year, np.PostDate.Value.Month })
                    .AsEnumerable();
                Dictionary<int, int[]> result = query.GroupBy(keySelector => keySelector.Year, val => val.Month)
                    .ToDictionary(key => key.Key, value => value.Distinct().ToArray());
                return result;
            });
        }

        private static string GetCacheKey(int id, int? categoryId = null)
        {
            return $"news_post_{id}{GetKeyComponent(categoryId)}";
        }

        private static string GetCacheKey(int? year = null, int? month = null, int? categoryId = null)
        {
            return $"news_post_all{GetKeyComponent(year)}{GetKeyComponent(month)}{GetKeyComponent(categoryId)}";
        }

        private static string GetCacheKeyPostDate(int? categoryId = null)
        {
            return nameof(GetPostsDateDictionary) + GetKeyComponent(categoryId);
        }

        private static string GetKeyComponent(int? value)
        {
            return value.HasValue ? $"_{value}" : string.Empty;
        }

        private NewsCategoryDto Map(Postgre.DAL.NewsCategory category)
        {
            if (category == null)
            {
                return null;
            }
            return new NewsCategoryDto
            {
                Id = category.Id,
                Title = category.Title,
                AlternativeTitle = category.AlternativeTitle,
                Alias = category.Alias,
                ShowOnStart = category.ShowOnStart ?? false,
                SortOrder = category.SortOrder
            };
        }

        private NewsPostDto Map(Postgre.DAL.NewsPost post)
        {
            if (post == null)
            {
                return null;
            }
            return new NewsPostDto
            {
                Id = post.Id,
                Title = post.Title,
                PostDate = DateOnly.FromDateTime(post.PostDate.GetValueOrDefault(new DateTime(2001, 01, 01))),
                Brief = post.Brief,
                Text = post.Text,
                Category = Map(post.Category),
                Published = post.StatusTypeId == 143
            };
        }
    }
}
