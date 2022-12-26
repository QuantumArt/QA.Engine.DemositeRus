using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface INewsService
    {
        IEnumerable<NewsPostDto> GetAllPosts(int? year = null, int? month = null, int? categoryId = null);
        NewsPostDto GetPost(int id, int? categoryId = null);
        IEnumerable<NewsCategoryDto> GetCategories();
        NewsPostDto[] GetPosts(int[] ids);
        IEnumerable<NewsPostDto> GetAllPosts(PostRequest request, int[] categoryIds = null);
        Dictionary<int, int[]> GetPostDateDictionary();
    }
}
