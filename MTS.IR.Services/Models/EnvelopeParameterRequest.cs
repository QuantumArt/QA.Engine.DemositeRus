using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.IR.Services.Models
{
    public class EnvelopeParameterRequest
    {
        public int? DistributionId { get; set; }
        public int[] EnvelopeIds { get; set; } = new int[0];
        public bool IncludeEnvelopesWithExceededSend { get; set; } = false;
        public bool IncludeEnvelopeNotSending { get; set; } = false;
    }
}
