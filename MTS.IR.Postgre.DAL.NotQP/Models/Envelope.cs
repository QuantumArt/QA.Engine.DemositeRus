using MTS.IR.Interfaces.Dto.Enums;

namespace MTS.IR.Postgre.DAL.NotQP.Models
{
    public class Envelope
    {
        public int Id { get; set; }
        public SendStatus Status { get; set; }
        public int? StatusCodeSMTP { get; set; }
        public int NumberOfAttempts { get; set; }
        public string Body { get; set; }
        public int SubscriberId { get; set; }
        public virtual EmailNewsSubscriber Subscriber { get; set; }
        public int DistributionId { get; set; }
        public virtual Distribution Distribution { get; set; }
    }
}
