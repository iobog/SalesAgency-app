
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.User;

namespace SalesAgency.Api.Controller.User;


[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public UserController(AppDbContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetAllProducts()
  { 
    return Ok(_mapper.Map<IEnumerable<User>>(await _db.TUser.ToListAsync()));
  }

}