using System;

namespace MTS.IR.Interfaces.Dto.Request
{
    public class PostRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
