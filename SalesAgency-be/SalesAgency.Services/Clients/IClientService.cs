using SalesAgency.Entities.DTOs.Client;

namespace SalesAgency.Services.Clients;

public interface IClientService 
{
  Task<List<GetClientDTO>> ListClientsAsync();
}