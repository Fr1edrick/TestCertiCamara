using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestCertiCamara.Repository
{
  public interface IRepositorieBaseline<T>
  {
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
  }
}
