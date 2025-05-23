namespace SalesAgency.Entities.Profiles;
using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Client;



public class ClientProfile : Profile
{
  public ClientProfile()
  {
    CreateMap<TClient, ClientDTO>();
  }
}
