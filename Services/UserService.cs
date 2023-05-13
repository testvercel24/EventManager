using NLog;
using CsvHelper;
using AutoMapper;
using Repository;
using Entity.Data;
using Entity.Dtos;
using Entity.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
namespace Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
    {
      _userRepository = userRepository;
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
      _logger.Info("Started uploading userIds and userNames for file");
      string extension = Path.GetExtension(file.FileName);
      if (extension.ToLower() != ".csv")
      {
        _logger.Error("File format is not as csv format");
        throw new CustomException(100, "Bad request", "Invalid file format");
      }
      try
      {
        _logger.Debug("Assigning file {0} details to UserModel", file);

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          var users = csv.GetRecords<UserModel>().ToList();
          _logger.Debug("Uploding UserModel details {0}", users);

          bool upload = _userRepository.UploadUser(users);
          _logger.Info("Successfully uploaded details users");
          return true;
        }
      }
      catch
      {
        _logger.Error("Invalid csv file format without proper headers or empty rows bad request");
        throw new CustomException(400, "Bad request", "Give proper Headers as UserId and UserName and remove empty rows");
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
      _logger.Info("Getting user details for the Id {0}", userId);

      UserModel? userModel = _userRepository.GetUserById(userId);
      if (userModel == null)
      {
        _logger.Error("No user details is found for Id");
        throw new CustomException(404, "Not Found", "User not found");
      }
      _logger.Debug("Mapping user model details {0} to user dto", userModel);

      UserDto user = _mapper.Map<UserDto>(userModel);

      _logger.Info("Successfully fetched user details");

      return user;
    }

    ///<summary>
    ///To get all the events for the given userId
    ///</summary>
    ///<param name=userId>Id to get all the associated event details</param>
    ///<result name=List<EventIdDto>Returns all the details of the events associated with userId</result>
    public List<EventIdDto> GetEventsForUser(int userId)
    {
      _logger.Info("Getting all the events for the user with Id");

      UserModel? userModel = _userRepository.GetUserById(userId);
      if (userModel == null)
      {
        _logger.Error("No user details is found for Id");
        throw new CustomException(404, "Not Found", "User not found");
      }
      _logger.Debug("Getting events for user with Id {0} from repository", userId);

      List<EventIdDto> events = _eventRepository.GetEventsForUser(userId);

      _logger.Info("Successfully fetched all the event details for user with Id");

      return events;
    }
  }
}