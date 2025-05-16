using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Product;

namespace SalesAgency.Entities.Profiles;
public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<TProduct, GetProductListItemDTO>();
  }
}