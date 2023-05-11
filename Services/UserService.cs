using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.Globalization;
using Entity.Models;
using Repository;
using Entity.Data;
using Microsoft.Extensions.Logging;
using Entity.Dtos;
using AutoMapper;
namespace Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IEventRepository _eventRepository;
    // private readonly AutoMapping _autoMapping;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IEventRepository eventRepository, ILogger<UserService> logger, IMapper mapper)
    {
      _userRepository = userRepository;
      _logger = logger;
      _mapper = mapper;
      _eventRepository = eventRepository;
    }

    ///<summary>
    ///To upload and map all the details like userIds and userNames ti the UserModel
    ///</summary>
    ///<param name=File>File that contains the details of the userIds and userNames</paam>
    ///<result name=bool>Returns true after succseful upload the details</result>
    public bool UploadUser(IFormFile file)
    {
      _logger.LogInformation("Started uploading userIds and userNames from {0}", file);
      try
      {
        _logger.LogDebug("Assigning file {0} details to UserModel", file);

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          var users = csv.GetRecords<UserModel>().ToList();
          _logger.LogDebug("Uploding UserModel details {0}", users);
          bool upload = _userRepository.UploadUser(users);
          _logger.LogInformation("Successfully uploaded details users {0}", users);
          return true;
        }
      }
      catch
      {
        _logger.LogError("Invalid csv file bad request");
        throw new CustomException(400, "Bad request", "Invalid Csv file format");
      }
    }

    ///<summary>
    ///To get all the active users available
    ///</summary>
    ///<param name=startIndex>To start the index for pagination</param>
    ///<param name=rowSize>Number of rows to be returned</param>
    ///<result name=List<UserDto>>Returns all the list of users details</result>
    public List<UserDto> GetAllUsers(int startIndex, int rowSize)
    {
      List<UserModel> userModels = _userRepository.GetAllUsers(startIndex, rowSize);
      List<UserDto> userDtoList = _mapper.Map<List<UserDto>>(userModels);
      return userDtoList;
    }

    ///<summary>
    ///To get user details for particular user-id
    ///</summary>
    ///<param name=userId>Id of the user for which the details to be fetched</param>
    ///<result name=UserDto>Returns all details of the user with userId</result>
    public UserDto GetUserById(int userId)
    {
      UserModel? userModel = _userRepository.GetUserById(userId);
      if (userModel == null)
      {
        throw new CustomException(404, "Not Found", "User not found");
      }
      UserDto user = _mapper.Map<UserDto>(userModel);
      return user;
    }

    ///<summary>
    ///To get all the events for the given userId
    ///</summary>
    ///<param name=userId>Id to get all the associated event details</param>
    ///<result name=List<EventIdDto>Returns all the details of the events associated with userId</result>
    public List<EventIdDto> GetEventsForUser(int userId)
    {
      List<EventIdDto> events = _eventRepository.GetEventsForUser(userId);
      return events;
    }
  }
}