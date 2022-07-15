using AutoMapper;
using TestCertiCamara.Models.CustomEntities;
using TestCertiCamara.Models.Entities;

namespace TestCertiCamara.Models.DTOs
{
  public class Mapping : Profile
  {
    public Mapping()
    {
      CreateMap<HistoryQueryLog, HistoryQueryLogDTO>().ReverseMap();
      CreateMap<HistoryQueryLogCreateDTO, HistoryQueryLog>().ReverseMap();
      CreateMap<Product, ProductDTO>().ReverseMap();
      CreateMap<Client, ClientDTO>().ReverseMap();
      CreateMap<(TransactionDTO, Client), MovementTransaction>()
        .ForMember(dest => dest.IdClient, opc => opc.MapFrom(src => src.Item2.Id))
        .ForMember(dest => dest.QuantityPayed, opc => opc.MapFrom(src => src.Item1.QuatityQuote))
        .ForMember(dest => dest.StatusPayed, opc => opc.MapFrom(src => 1));
      CreateMap<(Client, MovementTransaction), ResponseMovementTransaction>()
        .ForMember(dest => dest.Id, opc => opc.MapFrom(src => src.Item1.Id))
        .ForMember(dest => dest.ClientName, opc => opc.MapFrom(src => src.Item1.ClientName))
        .ForMember(dest => dest.ClientEmail, opc => opc.MapFrom(src => src.Item1.ClientEmail))
        .ForMember(dest => dest.IdProduct, opc => opc.MapFrom(src => src.Item1.IdProduct))
        .ForMember(dest => dest.DatePayed, opc => opc.MapFrom(src => src.Item2.DatePayed))
        .ForMember(dest => dest.QuantityPayed, opc => opc.MapFrom(src => src.Item2.QuantityPayed))
        .ForMember(dest => dest.StatusPayed, opc => opc.MapFrom(src => src.Item2.StatusPayed));
    }
  }
}
