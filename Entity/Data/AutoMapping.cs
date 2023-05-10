using AutoMapper;
using Entity.Models;
using Entity.Dtos;
namespace Entity.Data
{
  public class AutoMapping : Profile
  {
    public AutoMapping()
    {
      var config = new MapperConfiguration(cfg =>
        {
          cfg.CreateMap<int, EventAttendeeModel>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src))
        .ForMember(dest => dest.EventId, opt => opt.Ignore());
        });
      CreateMap<UserModel, UserDto>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
    }
  }
}