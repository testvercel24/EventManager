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

namespace UnitTesting
{
  public class BaseTesting
  {
    public IConfiguration _config;
    public ILogger _logger;
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
      _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

      ServiceProvider serviceProvider = new ServiceCollection()
      .AddLogging()
      .BuildServiceProvider();

      ILoggerFactory _loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
      _logger = _loggerFactory.CreateLogger(typeof(EventController));

      _mapper = serviceProvider.GetService<IMapper>()!;
      optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb");
      var options = new DbContextOptionsBuilder<ApiDbContext>()
          .UseInMemoryDatabase(databaseName: "training-vishnu")
          .Options;
      _context = new ApiDbContext(optionsBuilder.Options);
      _context.SaveChanges();

      _context.Users.AddRange(new UserModel[]
      {
        new UserModel{UserId=1,UserName="vishnu"},
        new UserModel{UserId=2,UserName="propel"}
      });
      _context.SaveChanges();

      _context.Events.AddRange(new EventModel[]
      {
        new EventModel{Id=EventId,EventName="Integration Session",StartDateTime=new DateTime(2023,05,09,14,00,00),EndDateTime=new DateTime(2023,05,09,14,30,00)}
      });
      _context.SaveChanges();
      _logger = _loggerFactory.CreateLogger(typeof(UserRepository));
      _userRepository = new UserRepository(_context, (ILogger<UserRepository>)_logger);
      _eventRepository = new EventRepository(_context);
      _userService = new UserService(_userRepository, (ILogger<UserService>)_logger);
      _eventService = new EventService(_eventRepository, _userService, _mapper, (ILogger<EventService>)_logger);
      _eventController = new EventController(_config, _eventService, (ILogger<EventController>)_logger);
      _userController = new UserController(_config, _userService, (ILogger<UserController>)_logger);
    }
  }
}