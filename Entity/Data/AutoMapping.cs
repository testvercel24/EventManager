using AutoMapper;
using Entity.Models;
using Entity.Dtos;
namespace Entity.Data
{
  [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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

      var userModelDto = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<UserModel, UserDto>()
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
      });
      CreateMap<UserModel, UserDto>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
    }
    public static void Configure(IMapperConfigurationExpression config)
    {
      config.CreateMap<UserModel, UserDto>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
      config.CreateMap<EventDto, EventModel>();
      config.CreateMap<int, EventAttendeeModel>()
         .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src))
         .ForMember(dest => dest.EventId, opt => opt.Ignore());
      config.CreateMap<EventModel, EventIdDto>();
    }

  }
}