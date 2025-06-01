
namespace SalesAgency.Api.Controller.Client;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Client;

[ApiController]
[Route("api/clients")]
[Authorize]
public class ClientController:ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public ClientController(AppDbContext dbContext, IMapper mapper)
  {
    _db = dbContext;
    _mapper = mapper;
  }

  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ClientDTO>>>GetAllClients()
  {
    return Ok(_mapper.Map<IEnumerable<ClientDTO>>(await _db.TClients.ToListAsync()));
  }


  
  [HttpGet("{id}",Name = "GetClientById")]
  public async Task<ActionResult<ClientDTO>>GetClientById(int id)
  {
    try
    {
      var client = await _db.TClients
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();
      
      if(client == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<ClientDTO>(client));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }

  }

  [HttpPost]
  public async Task<ActionResult<TClient>> CreateClient(TClient createUpdateClient)
  {
    try
    {
      await _db.AddAsync(createUpdateClient);
      await _db.SaveChangesAsync();

      var clientRead = _mapper.Map<ClientDTO>(createUpdateClient);

      return CreatedAtRoute(nameof(GetClientById),new {Id = clientRead.Id},clientRead);

    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
    }
  }


}