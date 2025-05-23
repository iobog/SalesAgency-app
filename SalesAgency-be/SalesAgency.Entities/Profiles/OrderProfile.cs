using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Order;

namespace SalesAgency.Entities.Profiles;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<TOrder, GetOrderDTO>();

    CreateMap<TOrder, GetOrderListItemDTO>()
      .ForMember(
        destinationMember => destinationMember.CountProducts,
        memberOptions => memberOptions.MapFrom(
          sourceMember => sourceMember.TOrderProducts.Count
        )
      )
      .ForMember(
        destinationMember => destinationMember.Client,
        memberOptions => memberOptions.MapFrom(
          sourceMember => sourceMember.Client.Name
        )
      );;


    CreateMap<CreateUpdateOrderDTO, TOrder>();
  }
}

