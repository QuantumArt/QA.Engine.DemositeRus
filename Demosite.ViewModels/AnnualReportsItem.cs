using System.Collections.Generic;

namespace Demosite.ViewModels
{
    public class AnnualReportsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string AdditionalAttachedImageUrl { get; set; }
        public List<AnnualReportsFileItem> Files { get; set; } = new();
    }
}
