using System;
using System.Collections.Generic;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Logic.Contracts
{
  public interface IProductLogic
  {
    List<Product> GetAllProducts();
    void CreditWithdraw(Guid idProduct, decimal withdraw);
  }
}
