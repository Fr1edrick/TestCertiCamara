using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestCertiCamara.Logic.Contracts;
using TestCertiCamara.Models;
using TestCertiCamara.Models.CustomEntities;
using TestCertiCamara.Models.DTOs;
using TestCertiCamara.Models.Entities;
using TestCertiCamara.Repository;

namespace TestCertiCamara.Logic.Implementations
{
  public class HistoryQueryLogLogic : RepositoryBaseline<HistoryQueryLog>, IHistoryQueryLogLogic
  {
    public HistoryQueryLogLogic(LogQueriesContext ctx) : base(ctx) { }
    public void Add(HistoryQueryLog dataToCreate)
    {
      Create(dataToCreate);
    }
    public List<HistoryQueryLog> GetAll()
    {
      return FindAll().OrderBy(x => x.Id).ToList();
    }
    public HistoryQueryLog GetById(Guid id)
    {
      return FindByCondition(x => x.Id.Equals(id)).FirstOrDefault();
    }
    public ResultQtyQueriesDTO GetTotalQueries()
    {
      var totalRows = FindAll().ToList().Count;
      ResultQtyQueriesDTO total = new() { TotalRetrieve = totalRows };
      return total;
    }
    public async Task<ResponseHttpClient> SendNumberRequest(int numberRequest)
    {
      try
      {
        var request = new NumberRequest() { ubiNum = numberRequest };
        return await GetNumberService(request);
      }
      catch (Exception)
      {
        throw;
      }
    }

    private async Task<ResponseHttpClient> GetNumberService(NumberRequest number)
    {
      try
      {
        var urlConfig = GetConfigUrlString();
        var json = JsonConvert.SerializeObject(number);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var url = string.Concat(urlConfig, "NumberToWords");

        using var clientHttp = new HttpClient();
        var response = await clientHttp.PostAsync(url, data);
        var result = await response.Content.ReadAsStringAsync();
        var dataToCreate = new HistoryQueryLog();
        if (response.ReasonPhrase.Equals("OK"))
        {
          dataToCreate.UrlQuery = url;
          dataToCreate.ResponseQuery = result.Replace("\"","").Trim();
          dataToCreate.RegistryDate = DateTime.Now;
        }
        return new ResponseHttpClient() { Result = result, Data = dataToCreate };
      }
      catch (Exception)
      {
        throw;
      }
    }
    private string GetConfigUrlString()
    {
      var settingsUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
      return settingsUrl.GetValue<string>("ServiceSoap:Url");
    }
  }
}
