using System;
using System.Collections.Generic;

#nullable disable

namespace TestCertiCamara.Models.Entities
{
    public partial class MovementTransaction
    {
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public decimal QuantityPayed { get; set; }
        public DateTime DatePayed { get; set; }
        public int StatusPayed { get; set; }

        public virtual Client IdClientNavigation { get; set; }
    }
}
