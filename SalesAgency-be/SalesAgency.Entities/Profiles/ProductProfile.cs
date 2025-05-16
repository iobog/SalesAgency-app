using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Product;

namespace SalesAgency.Entities.Profiles;

public class ProductPorfile: Profile
{
  public ProductPorfile()
  {
    CreateMap<TProduct,GetProductDTO>();
    CreateMap<CreateUpdateProductDTO,TProduct>();
  }
}