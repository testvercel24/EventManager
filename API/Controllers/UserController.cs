using NLog;
using Services;
using Entity.Data;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace Controller
{
  [Route("api/users")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public UserController(IUserService userService)
    {
      _userService = userService;
    }
    [HttpPost]
    ///<summar>
    /// To upload the list of userIds and userNames
    ///</summary>
    ///<param name=File>The file which contains all the userIds and userNames</param>
    ///<result>201 created status</result>
    [SwaggerResponse(201, "Successfull created")]
    [SwaggerResponse(400, "Bad Request", typeof(CustomException))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult UploadUser(IFormFile file)
    {
      _logger.Info("Started uploding {0} details", file);
      _logger.Info("IP address: {ipAddress}, Endpoint: {endpoint}", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);


      _userService.UploadUser(file);

      _logger.Info("Successfully uploaded the {0} details", file);

      return Ok();
    }

    [HttpGet]
    ///<summary>
    ///To get all the users from user entity
    ///</summary>
    ///<param name=start-index>Starting of the index for pagination</param>
    ///<param name=row-size>Number of rows to be returned</param>
    ///<result name=List<UserDto>>Returns all the active users</result>
    [SwaggerResponse(200, "Successfully", typeof(List<UserDto>))]
    [SwaggerResponse(500, "Internal server Error")]
    public IActionResult GetAllUsers([FromQuery(Name = "offset")] int startIndex = 0, [FromQuery(Name = "size")] int rowSize = 5)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Fetching all users", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      List<UserDto> users = _userService.GetAllUsers(startIndex, rowSize);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully fetched all the users", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return Ok(users);
    }


    [HttpGet("{user-id}")]
    ///<summary>
    ///To get particular user by Id
    ///</summary>
    ///<param name=user-id>Id of the user to get user details</param>
    ///<result name=UserDto>Return details of user such as UserId and UserName</result>
    [SwaggerResponse(200, "Successfully fetches user details", typeof(UserDto))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult GetUserById([FromRoute(Name = "user-id")] int userId)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Getting user details for Id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      UserDto user = _userService.GetUserById(userId);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully fetched the user details", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return Ok(user);
    }


    [HttpGet("{user-id}/events")]
    ///<summary>
    ///To get all the events for the user
    ///</summary>
    ///<param name=user-id>Id of the user to get all the event mapped with</param>
    ///<result name=List<EventDto>>COntains list of event details like eventId, eventName,startDateTime,endDateTime</result>
    [SwaggerResponse(200, "successfully fetched all the events", typeof(List<EventDto>))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult GetEventsForUser([FromRoute(Name = "user-id")] int userId)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Getting Events for the user with Id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      List<EventIdDto> events = _userService.GetEventsForUser(userId);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully fetched the events for user with Id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return Ok(events);
    }
  }
}