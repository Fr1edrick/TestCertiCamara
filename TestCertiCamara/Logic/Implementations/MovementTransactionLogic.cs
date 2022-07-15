using System;
using System.Linq;
using TestCertiCamara.Logic.Contracts;
using TestCertiCamara.Models;
using TestCertiCamara.Models.Entities;
using TestCertiCamara.Repository;

namespace TestCertiCamara.Logic.Implementations
{
  public class MovementTransactionLogic : RepositoryBaseline<MovementTransaction>, IMovementTransactionLogic
  {
    public MovementTransactionLogic(LogQueriesContext ctx) : base(ctx) { }
    public void Add(MovementTransaction transaction)
    {
      Create(transaction);
    }
    public bool GetQuantityDailyTransaction(Guid idClient)
    {
      var lastDate = GetLastDate(idClient);
      var qtyTransaction = FindByCondition(x => x.IdClient.Equals(idClient) && x.DatePayed == lastDate)
        .ToList().Count;
      return qtyTransaction > 5 ? true : false;
    }
    public bool GetQuantityDailyTransactionGreaterThanFiveMillions(Guid idClient)
    {
      var lastDate = GetLastDate(idClient);
      var dailyMount = FindByCondition(x => x.IdClient.Equals(idClient) && x.DatePayed == lastDate)
        .ToList()
        .Select(x => x.QuantityPayed)
        .Sum();
      return dailyMount > 5_000_000 ? true : false;
    }
    private DateTime GetLastDate(Guid idClient)
    {
      return FindByCondition(x => x.IdClient == idClient)
        .ToList()
        .OrderByDescending(x => x.DatePayed)
        .Take(1)
        .Select(x => x.DatePayed)
        .FirstOrDefault();
    }
  }
}
