using Demosite.Interfaces.Dto.Enums;
using System;
using System.Collections.Generic;

namespace Demosite.Postgre.DAL.NotQP.Models
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
