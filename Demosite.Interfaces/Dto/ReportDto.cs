using System.Collections.Generic;

namespace Demosite.Interfaces.Dto
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string AdditionalAttachedImageUrl { get; set; }
        public IEnumerable<ReportFileDto> ReportFiles { get; set; }
    }
}
