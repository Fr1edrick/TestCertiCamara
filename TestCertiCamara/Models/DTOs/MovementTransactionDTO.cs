using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestCertiCamara.Models.DTOs
{
  public class MovementTransactionDTO
  {
    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public decimal QuantityPayed { get; set; }
    public DateTime DatePayed { get; set; }
    public int StatusPayed { get; set; }
  }
  public class MovementTransactionCreateDTO
  {
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El Id del Cliente es requerido")]
    [RegularExpression("^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$", ErrorMessage = "Indique un Guid válido.")]
    public Guid IdClient { get; set; }

    [Required(ErrorMessage = "La cantidad a pagar es requerida")]
    public decimal QuantityPayed { get; set; }
    public DateTime DatePayed { get; set; }
    public int StatusPayed { get; set; }
  }
  public class ProductDTO
  {
    public Guid Id { get; set; }
    public string NameProduct { get; set; }
    public decimal MountCredit { get; set; }
    public int NumberQuotas { get; set; }
    public decimal PriceQuota { get; set; }
  }
  public class TransactionDTO
  {
    [Required(ErrorMessage = "Los Nombre del cliente son requeridos")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El numero de la tarjeta es requerido")]
    public Guid CardTarget { get; set; }

    [Required(ErrorMessage = "La fecha de Expiración de la tarjeta es requerido")]
    [DataType(DataType.Date, ErrorMessage = "La fecha es incorrecta")]
    public DateTime CardExpire { get; set; }

    [Required(ErrorMessage = "El valor a pagar es requerido")]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "No se admite valores negativos")]
    public decimal QuatityQuote { get; set; }

    [Required(ErrorMessage = "El Email del cliente es requerido")]
    [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Indique un correo electrónico válido.")]
    public string Email { get; set; }

    public Guid IdProduct { get; set; }
  }
  public class ClientDTO
  {
    public Guid Id { get; set; }
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public Guid IdProduct { get; set; }
  }
}
