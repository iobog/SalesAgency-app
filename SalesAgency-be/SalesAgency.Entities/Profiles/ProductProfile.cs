using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Product;

namespace SalesAgency.Entities.Profiles;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<TProduct, GetProductDTO>();
  }
}