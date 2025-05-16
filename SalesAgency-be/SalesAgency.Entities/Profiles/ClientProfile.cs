namespace SalesAgency.Entities.Profiles;
using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Client;

public class ClientPorfile: Profile
{
  public ClientPorfile()
  {
    CreateMap<TClient,GetClientDTO>();
    
  }
}