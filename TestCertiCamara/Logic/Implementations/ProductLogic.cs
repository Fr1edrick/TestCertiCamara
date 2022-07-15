using System;
using System.Collections.Generic;
using System.Linq;
using TestCertiCamara.Logic.Contracts;
using TestCertiCamara.Models;
using TestCertiCamara.Models.Entities;
using TestCertiCamara.Repository;

namespace TestCertiCamara.Logic.Implementations
{
  public class ProductLogic : RepositoryBaseline<Product>, IProductLogic
  {
    public ProductLogic(LogQueriesContext ctx) : base(ctx) { }
    public List<Product> GetAllProducts()
    {
      return FindAll().ToList();
    }
    public void CreditWithdraw(Guid idProduct, decimal withdraw)
    {
      var totalMount = FindByCondition(x => x.Id.Equals(idProduct)).FirstOrDefault();
      var payCopy = totalMount;
      payCopy.Balance -= withdraw;
      Update(payCopy);

    }
  }
}
