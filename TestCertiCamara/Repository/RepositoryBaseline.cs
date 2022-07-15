using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TestCertiCamara.Models;

namespace TestCertiCamara.Repository
{
  public abstract class RepositoryBaseline<T> : IRepositorieBaseline<T> where T : class
  {
    protected readonly LogQueriesContext _context;
    public RepositoryBaseline(LogQueriesContext ctx) => _context = ctx;
    public void Create(T entity) => _context.Set<T>().Add(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).AsNoTracking();
  }
}
