using System;
using System.Collections.Generic;

#nullable disable

namespace TestCertiCamara.Models.Entities
{
    public partial class Client
    {
        public Client()
        {
            MovementTransactions = new HashSet<MovementTransaction>();
        }

        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public Guid IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual ICollection<MovementTransaction> MovementTransactions { get; set; }
    }
}
