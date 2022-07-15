using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestCertiCamara.Logic;
using TestCertiCamara.Models.CustomEntities;
using TestCertiCamara.Models.DTOs;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Controllers
{
  [Route("v1/api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class TransactionalZoneController : Controller
  {
    private IUnitOfWork _repository;
    private IMapper _mapper;
    public TransactionalZoneController(IUnitOfWork repo, IMapper mapper)
    {
      _repository = repo;
      _mapper = mapper;
    }

    [HttpGet("GetProducts")]
    public IActionResult GetProducts()
    {
      try
      {
        var lstProducts = _repository.Product.GetAllProducts();
        if (lstProducts is null) return BadRequest("No existen productos");
        return Ok(_mapper.Map<List<ProductDTO>>(lstProducts));
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
      }
    }
    [HttpPost("PayProduct")]
    public IActionResult PayProduct([FromBody] TransactionDTO transaction)
    {
      try
      {
        if (transaction is null) return BadRequest("El JSON es requerido");
        if (!ModelState.IsValid) return BadRequest("Cuerpo de la solicitud incorrecto");
        var existUser = _repository.Client.GetByNameClient(transaction.Name, transaction.Email);
        if (existUser is null) return NotFound($"No existe usuario con el cliente: {transaction.Name}");
        var data = ProcessData(transaction, existUser);
        if (transaction.QuatityQuote >= 10_000_000 || GetDailyTransactionGreaterThanFiveMillions(existUser.Id) || GetDailyPayedGreaterThanFive(existUser.Id))
        {
          data.StatusPayed = 0;
          _repository.MovementTransaction.Add(data);
          _repository.Save();
          return Ok(_mapper.Map<ResponseMovementTransaction>((existUser, data)));
        }
        _repository.MovementTransaction.Add(data);
        _repository.Product.CreditWithdraw(transaction.IdProduct, transaction.QuatityQuote);
        _repository.Save();
        return Ok(_mapper.Map<ResponseMovementTransaction>((existUser, data)));
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
      }
    }
    private bool GetDailyPayedGreaterThanFive(Guid idClient) => _repository.MovementTransaction.GetQuantityDailyTransaction(idClient);
    private bool GetDailyTransactionGreaterThanFiveMillions(Guid idClient) => _repository.MovementTransaction.GetQuantityDailyTransactionGreaterThanFiveMillions(idClient);
    private MovementTransaction ProcessData(TransactionDTO transaction, Client client) => _mapper.Map<MovementTransaction>((transaction, client));
  }
}
