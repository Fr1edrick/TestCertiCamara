using System;
using System.Linq;
using TestCertiCamara.Logic.Contracts;
using TestCertiCamara.Models;
using TestCertiCamara.Models.Entities;
using TestCertiCamara.Repository;

namespace TestCertiCamara.Logic.Implementations
{
  public class ClientLogic : RepositoryBaseline<Client>, IClientLogic
  {
    public ClientLogic(LogQueriesContext ctx) : base(ctx) { }
    public Client GetByNameClient(string name, string email)
    {
      try
      {
        return FindByCondition(x => x.ClientName.Equals(name) && x.ClientEmail.Equals(email)).FirstOrDefault();
      }
      catch (Exception)
      {
        throw;
      }
    }

  }
}
