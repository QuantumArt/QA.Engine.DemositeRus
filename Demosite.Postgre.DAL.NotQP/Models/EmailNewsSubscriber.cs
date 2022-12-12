using System;
using System.Collections.Generic;

namespace Demosite.Postgre.DAL.NotQP.Models
{
    public class EmailNewsSubscriber
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = false;
        public string ConfirmCode { get; set; }
        public DateTime? ConfirmCodeSendDate { get; set; }
        public int[] NewsCategory { get; set; }
        public virtual ICollection<Envelope> Envelopes { get; set; }
    }
}
