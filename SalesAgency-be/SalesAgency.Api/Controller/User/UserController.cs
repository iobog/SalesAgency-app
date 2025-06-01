
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesAgency.Entities;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Login;
using SalesAgency.Entities.DTO.User;

namespace SalesAgency.Api.Controller.User;


[ApiController]
[Route("api/")]
public class UserController : ControllerBase
{

  private readonly AppDbContext _db;
  private readonly IMapper _mapper;
  private readonly IConfiguration _configuration;

  public UserController(AppDbContext db, IMapper mapper, IConfiguration configuration)
  {
    _db = db;
    _mapper = mapper;
    _configuration = configuration;
  }

  [HttpPost("login")]
  public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginDto)
  {
    var user = await _db.TUsers
      .FirstOrDefaultAsync(u => u.Email == loginDto.User && u.Password == loginDto.Pass);

    if (user == null)
      return Unauthorized("Invalid credentials.");

    var token = GenerateJwtToken(user);

    return Ok(new LoginResponseDTO
    {
      Email = user.Email ?? "",
      Token = token
    });
  }

  // https://medium.com/@solomongetachew112/jwt-authentication-in-net-8-a-complete-guide-for-secure-and-scalable-applications-6281e5e8667c
  private string GenerateJwtToken(TUser user)
  {    
    var claims = new[]
    {
        new Claim("UserId", user.Id.ToString()),
        new Claim("UserEmail", user.Email!),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtToken")!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "yourdomain.com",
        audience: "yourdomain.com",
        claims: claims,
        expires: DateTime.Now.AddDays(30),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }


}