using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.User;
using SalesAgency.Entities.Login;

namespace SalesAgency.Entities.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<TUser, GetUserDTO>();
    CreateMap<TUser, LoginResponseDTO>();
  }
}
