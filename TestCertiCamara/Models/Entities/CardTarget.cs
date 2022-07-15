using System;
using System.Collections.Generic;

#nullable disable

namespace TestCertiCamara.Models.Entities
{
    public partial class CardTarget
    {
        public Guid Id { get; set; }
        public string NameCard { get; set; }
        public string CardType { get; set; }
        public DateTime? DateExpire { get; set; }
        public Guid IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
