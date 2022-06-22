using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IBlogService
    {
        IEnumerable<BlogPostDto> GetAllPosts();
        BlogPostDto GetPost(int id);

        IEnumerable<BlogTagDto> GetTags();

        IEnumerable<BlogCategoryDto> GetCategories();
    }
}
