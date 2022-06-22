using MTS.IR.Interfaces.Dto;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IBlogService
    {
        IEnumerable<BlogPostDto> GetAllPosts();
        BlogPostDto GetPost(int id);

        IEnumerable<BlogTagDto> GetTags();

        IEnumerable<BlogCategoryDto> GetCategories();
    }
}
