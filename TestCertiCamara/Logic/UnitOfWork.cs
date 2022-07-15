using TestCertiCamara.Logic.Contracts;
using TestCertiCamara.Logic.Implementations;
using TestCertiCamara.Models;

namespace TestCertiCamara.Logic
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly LogQueriesContext _context;
    private IHistoryQueryLogLogic _historyQuery;
    private IProductLogic _product;
    private IClientLogic _clientLogic;
    private IMovementTransactionLogic _movementTransaction;
    public UnitOfWork(LogQueriesContext ctx) => _context = ctx;

    public IHistoryQueryLogLogic HistoryQuery
    {
      get
      {
        if (_historyQuery is null)
          _historyQuery = new HistoryQueryLogLogic(_context);
        return _historyQuery;
      }
    }
    public IProductLogic Product
    {
      get
      {
        if (_product is null)
          _product = new ProductLogic(_context);
        return _product;
      }
    }
    public IClientLogic Client
    {
      get
      {
        if (_clientLogic is null)
          _clientLogic = new ClientLogic(_context);
        return _clientLogic;
      }
    }
    public IMovementTransactionLogic MovementTransaction
    {
      get
      {
        if (_movementTransaction is null)
          _movementTransaction = new MovementTransactionLogic(_context);
        return _movementTransaction;
      }
    }
    public void Save() => _context.SaveChanges();
  }
}
