using MTS.IR.Interfaces.Dto.Enums;
using System;
using System.Collections.Generic;

namespace MTS.IR.Postgre.DAL.NotQP.Models
{
    public class Distribution
    {
        public int Id { get; set; }
        public SendStatus Status { get; set; }
        public int[] NewsIds { get; set; }
        public DateTime? Created { get; set; }
        public virtual ICollection<Envelope> Envelopes { get; set; }
    }
}
