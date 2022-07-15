using System;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Logic.Contracts
{
  public interface IMovementTransactionLogic
  {
    bool GetQuantityDailyTransaction(Guid idClient);
    bool GetQuantityDailyTransactionGreaterThanFiveMillions(Guid idClient);
    void Add(MovementTransaction transaction);
  }
}
