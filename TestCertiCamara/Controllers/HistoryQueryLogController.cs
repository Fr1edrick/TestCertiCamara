using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestCertiCamara.Logic;
using TestCertiCamara.Models.DTOs;

namespace TestCertiCamara.Controllers
{
  [Route("api/v1/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class HistoryQueryLogController : ControllerBase
  {
    private readonly IUnitOfWork _repository;
    private readonly IMapper _mapper;

    public HistoryQueryLogController(IUnitOfWork repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    #region PRIMER EJERCICIO

    [HttpGet("GetById/{id:Guid}")]
    public IActionResult GetById(Guid id)
    {
      try
      {
        if (id == Guid.Empty) return BadRequest("Indique un Guid Válido");
        var dataRetrieve = _repository.HistoryQuery.GetById(id);
        if (dataRetrieve is not null)
          return Ok(_mapper.Map<HistoryQueryLogDTO>(dataRetrieve));
        return null;
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error: {ex.Message} {ex.StackTrace}");
      }
    }
    [HttpGet("GetTotalQueries")]
    public IActionResult GetTotalQueries()
    {
      try
      {
        var responseQty = _repository.HistoryQuery.GetTotalQueries();
        return Ok(responseQty);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
      }
    }
    [HttpPost()]
    public async Task<IActionResult> RequestNumber(int number = 0)
    {
      try
      {
        var result = await _repository.HistoryQuery.SendNumberRequest(number);
        _repository.HistoryQuery.Add(result.Data);
        _repository.Save();
        //var jsonResult = JsonConvert.DeserializeObject(result);

        return StatusCode(201, new { stringNumber = result });
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error {ex.Message}");
      }
    }

    #endregion
  }
}
