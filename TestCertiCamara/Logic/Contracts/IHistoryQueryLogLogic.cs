using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCertiCamara.Models.CustomEntities;
using TestCertiCamara.Models.DTOs;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Logic.Contracts
{
  public interface IHistoryQueryLogLogic
  {
    HistoryQueryLog GetById(Guid id);
    List<HistoryQueryLog> GetAll();
    ResultQtyQueriesDTO GetTotalQueries();
    void Add(HistoryQueryLog dataToCreate);
    Task<ResponseHttpClient> SendNumberRequest(int numberRequest);
  }
}
