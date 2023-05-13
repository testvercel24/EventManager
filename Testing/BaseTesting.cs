using Services;
using Moq;
using Repository;
using AutoMapper;
using Controller;
using Entity.Data;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Testing
{
  public class BaseTesting
  {
    public ApiDbContext _context;
    public Guid EventId;
    private readonly DbContextOptionsBuilder<ApiDbContext> optionsBuilder;
    public EventController _eventController;
    public EventController _eventController2;
    public EventRepository _eventRepository;
    public EventService _eventService;
    public UserController _userController;
    public UserService _userService;
    public UserRepository _userRepository;
    public DateTime startDateTime;
    public DateTime endDateTime;
    private readonly IMapper _mapper;
    public int user1Id = new Random().Next();
    public int user2Id = new Random().Next();

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
        new UserModel{UserId=user1Id,UserName="vishnu"},
        new UserModel{UserId=user2Id,UserName="propel"}
      });
      _context.SaveChanges();

      EventId = Guid.NewGuid();
      _context.Events.AddRange(new EventModel[]
      {
        new EventModel{Id=Guid.NewGuid(),EventName="Integration Session Past",StartDateTime=new DateTime(2023,05,11,14,00,00),EndDateTime=new DateTime(2023,05,11,14,30,00)},
        new EventModel{Id=EventId,EventName="Integration Session",StartDateTime=new DateTime(2024,05,14,14,00,00),EndDateTime=new DateTime(2024,05,14,14,30,00)},
      });
      _context.SaveChanges();
      _context.EventAttendees.AddRange(new EventAttendeeModel[]
      {
        new EventAttendeeModel{UserId=user1Id,EventId=EventId},
        new EventAttendeeModel{UserId=user1Id,EventId=EventId}
      });
      _context.SaveChanges();
      _userRepository = new UserRepository(_context);
      _eventRepository = new EventRepository(_context);
      _userService = new UserService(_userRepository, _eventRepository, _mapper);
      _eventService = new EventService(_eventRepository, _userService, _userRepository, _mapper);
      _eventController = new EventController(_eventService);
      _eventController2 = new EventController(_eventService);
      _userController = new UserController(_userService);

      // Create a mock HttpRequest
      var mockRequest = new Mock<HttpRequest>();
      mockRequest.Setup(r => r.Path).Returns("/your-endpoint");

      // Create a mock connection
      var mockConnection = new Mock<ConnectionInfo>();
      mockConnection.Setup(c => c.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

      // Create a mock HttpContext
      var mockHttpContext = new Mock<HttpContext>();
      mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
      mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);
      _eventController.ControllerContext = new ControllerContext
      {
        HttpContext = mockHttpContext.Object
      };
      _eventController2.ControllerContext = new ControllerContext
      {
        HttpContext = mockHttpContext.Object
      };
      _userController.ControllerContext = new ControllerContext
      {
        HttpContext = mockHttpContext.Object
      };
    }
  }
}