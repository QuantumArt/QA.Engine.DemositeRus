using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Interfaces.Dto
{
    public class BlogPostDto
    {
        public BlogPostDto()
        {
            Tags = Enumerable.Empty<BlogTagDto>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public DateTime PostDate { get; set; }
        public BlogCategoryDto Category { get; set; }
        public string Text { get; set; }
        public IEnumerable<BlogTagDto> Tags { get; set; }
        public string Image { get; set; }
        public bool Published { get; set; }
    }
}
