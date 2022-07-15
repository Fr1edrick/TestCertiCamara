using System;
using System.Collections.Generic;

#nullable disable

namespace TestCertiCamara.Models.Entities
{
    public partial class HistoryQueryLog
    {
        public Guid Id { get; set; }
        public string UrlQuery { get; set; }
        public string ResponseQuery { get; set; }
        public DateTime? RegistryDate { get; set; }
    }
}
