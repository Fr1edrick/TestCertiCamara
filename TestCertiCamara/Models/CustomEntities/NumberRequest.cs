using System;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Models.CustomEntities
{
  public class NumberRequest
  {
    public int ubiNum { get; set; }
  }
  public class ResponseHttpClient
  {
    public string Result { get; set; }
    public HistoryQueryLog Data { get; set; }
  }

  public class ResponseMovementTransaction
  {
    public Guid Id { get; set; }
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public Guid IdProduct { get; set; }
    public decimal QuantityPayed { get; set; }
    public DateTime DatePayed { get; set; }
    public int StatusPayed { get; set; }
  }
}
