using System;

namespace MTS.IR.Interfaces.Dto
{
    public class NewsPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public DateTime PostDate { get; set; }
        public NewsCategoryDto Category { get; set; }
        public string Text { get; set; }
        public bool Published { get; set; }
    }
}
