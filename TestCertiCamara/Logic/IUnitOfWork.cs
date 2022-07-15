using TestCertiCamara.Logic.Contracts;

namespace TestCertiCamara.Logic
{
  public interface IUnitOfWork
  {
    IHistoryQueryLogLogic HistoryQuery { get; }
    IProductLogic Product { get; }
    IClientLogic Client { get; }
    IMovementTransactionLogic MovementTransaction { get; }
    void Save();
  }
}
