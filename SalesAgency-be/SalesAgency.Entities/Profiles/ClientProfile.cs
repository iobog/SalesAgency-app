namespace SalesAgency.Entities.Profiles;
using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Client;



public class ClientProfile : Profile
{
  public ClientProfile()
  {
    CreateMap<TClient, GetClientListItemDTO>();
  }
}
