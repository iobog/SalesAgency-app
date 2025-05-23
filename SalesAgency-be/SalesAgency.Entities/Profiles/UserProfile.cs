using AutoMapper;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Login;
using SalesAgency.Entities.DTO.User;

namespace SalesAgency.Entities.Profiles;


public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<TUser, LoginResponseDTO>()
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    CreateMap<TUser, GetUserDTO>();
  }
}
