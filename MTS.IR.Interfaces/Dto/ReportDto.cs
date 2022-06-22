using System.Collections.Generic;

namespace MTS.IR.Interfaces.Dto
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
