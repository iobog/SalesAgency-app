
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Login;
using SalesAgency.Entities.DTO.User;

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
  public async Task<ActionResult<IEnumerable<TUser>>> GetAllUsers()
  {
    return Ok(_mapper.Map<IEnumerable<TUser>>(await _db.TUsers.ToListAsync()));
  }
  


  [HttpPost("login")]
  public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginDto)
  {
    var user = await _db.TUsers
      .FirstOrDefaultAsync(u => u.Email == loginDto.User && u.Password == loginDto.Pass);

    if (user == null)
      return Unauthorized("Invalid credentials.");

    var token = Guid.NewGuid().ToString();
    
    return Ok(new LoginResponseDTO
    {
      Email = user.Email ?? "",
      Token = token
    });
  }


}