namespace Demosite.Services.Models
{
    public class EnvelopeParameterRequest
    {
        public int? DistributionId { get; set; }
        public int[] EnvelopeIds { get; set; } = new int[0];
        public bool IncludeEnvelopesWithExceededSend { get; set; } = false;
        public bool IncludeEnvelopeNotSending { get; set; } = false;
    }
}
