using Controller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Entity.Data;
using Microsoft.EntityFrameworkCore;
using Entity.Dtos;
using Entity.Models;
using Repository;
using Services;
using Microsoft.EntityFrameworkCore.InMemory;
using AutoMapper;
namespace Testing
{
  public class BaseTesting
  {
    public ApiDbContext _context;
    public Guid EventId = Guid.NewGuid();
    private readonly DbContextOptionsBuilder<ApiDbContext> optionsBuilder;
    public EventController _eventController;
    public EventRepository _eventRepository;
    public EventService _eventService;
    public UserController _userController;
    public UserService _userService;
    public UserRepository _userRepository;
    public DateTime startDateTime;
    public DateTime endDateTime;
    private readonly IMapper _mapper;
    public BaseTesting()
    {

      ServiceProvider serviceProvider = new ServiceCollection()
      .AddLogging()
      .BuildServiceProvider();
      _mapper = serviceProvider.GetService<IMapper>()!;
      var mapperConfiguration = new MapperConfiguration(cfg =>
      {
        // Configure your mappings here
        cfg.CreateMap<EventDto, EventModel>();
        cfg.CreateMap<EventModel, EventIdDto>();
        cfg.CreateMap<UserModel, UserDto>();
        cfg.CreateMap<int, EventAttendeeModel>();
      });
      _mapper = mapperConfiguration.CreateMapper();
      optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb");
      var options = new DbContextOptionsBuilder<ApiDbContext>()
          .UseInMemoryDatabase(databaseName: "training-vishnu")
          .Options;
      _context = new ApiDbContext(optionsBuilder.Options);
      _context.Users.AddRange(new UserModel[]
      {
        new UserModel{UserId=1,UserName="vishnu"},
        new UserModel{UserId=2,UserName="propel"}
      });
      _context.SaveChanges();

      _context.Events.AddRange(new EventModel[]
      {
        new EventModel{Id=EventId,EventName="Integration Session",StartDateTime=new DateTime(2023,05,14,14,00,00),EndDateTime=new DateTime(2023,05,14,14,30,00)}
      });
      _context.SaveChanges();
      _context.EventAttendees.AddRange(new EventAttendeeModel[]
      {
        new EventAttendeeModel{UserId=1,EventId=EventId},
        new EventAttendeeModel{UserId=2,EventId=EventId}
      });
      _context.SaveChanges();
      _userRepository = new UserRepository(_context);
      _eventRepository = new EventRepository(_context);
      _userService = new UserService(_userRepository, _eventRepository, _mapper);
      _eventService = new EventService(_eventRepository, _userService, _userRepository, _mapper);
      _eventController = new EventController(_eventService);
      _userController = new UserController(_userService);
    }
  }
}