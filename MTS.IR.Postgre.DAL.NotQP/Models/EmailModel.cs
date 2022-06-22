using MTS.IR.Interfaces.Dto;

namespace MTS.IR.Postgre.DAL.NotQP.Models
{
    public class EmailModel
    {
        public Subscriber Subscriber { get; set; }
        public NewsPostDto[] NewsPosts { get; set; }
        public string BaseUrl { get; set; }
        public string LogoImage { get; set; }
    }
}
