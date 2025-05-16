using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Order;

namespace SalesAgency.Entities.Profiles;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<TOrder, GetOrderListItemDTO>()
      .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client.Name))
      .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
      .ForMember(dest => dest.CountProducts, opt => opt.MapFrom(src => src.TOrderProducts.Count));
    CreateMap<TOrderProduct, CreateOrderProductDTO>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
      .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
      .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
  }
}
