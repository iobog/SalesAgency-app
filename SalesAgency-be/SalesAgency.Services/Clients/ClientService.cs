using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Client;

namespace SalesAgency.Services.Clients;

public class ClientService : IClientService
{
  private readonly AppDbContext _db;

  public ClientService(AppDbContext db)
  {
    _db = db;  
  }

  public async Task<List<GetClientDTO>> ListClientsAsync()
  {
    return await _db.TClients
      .Select(_ => new GetClientDTO()
      {
        Id = _.Id,
        Name = _.Name
      })
      .ToListAsync();
  }
}