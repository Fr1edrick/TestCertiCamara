using System;
using System.Collections.Generic;

#nullable disable

namespace TestCertiCamara.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            Clients = new HashSet<Client>();
        }

        public Guid Id { get; set; }
        public string NameProduct { get; set; }
        public decimal? MountCredit { get; set; }
        public decimal? Balance { get; set; }
        public int? NumberQuotas { get; set; }
        public decimal? PriceQuota { get; set; }

        public virtual CardTarget CardTarget { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
