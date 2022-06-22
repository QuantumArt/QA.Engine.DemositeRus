namespace Demosite.Interfaces.Dto
{
    public class NewsCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AlternativeTitle { get; set; }
        public string Alias { get; set; }
        public bool ShowOnStart { get; set; }
        public int? SortOrder { get; set; }
    }
}
