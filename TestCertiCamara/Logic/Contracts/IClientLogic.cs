using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Logic.Contracts
{
  public interface IClientLogic
  {
    Client GetByNameClient(string name, string email);
  }
}
