using System;

namespace TestCertiCamara.Models.DTOs
{
    public class HistoryQueryLogDTO
    {
        public Guid Id { get; set; }
        public string UrlQuery { get; set; }
        public string ResponseQuery { get; set; }
        public DateTime RegistryDate { get; set; }
    }
    public class HistoryQueryLogCreateDTO
    {
        public string UrlQuery { get; set; }
        public string ResponseQuery { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}
