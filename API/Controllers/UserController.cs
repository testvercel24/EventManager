using Microsoft.AspNetCore.Mvc;
using Services;
using Entity.Data;
using Swashbuckle.AspNetCore.Annotations;
using Entity.Dtos;
namespace Controller
{
  [Route("api/user")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly ILogger<UserController> _logger;
    public UserController(IConfiguration config, IUserService userService, ILogger<UserController> logger)
    {
      _config = config;
      _userService = userService;
      _logger = logger;
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
    public IActionResult UploadUser([FromForm(Name = "File")] IFormFile file)
    {
      _logger.LogInformation("Started uploding {0} details", file);
      bool upload = _userService.UploadUser(file);
      if (upload == true)
      {
        _logger.LogInformation("Successfully uploaded the {0} details", file);
        return Ok();
      }
      else
      {
        _logger.LogError("Internal server error is thrown");
        throw new CustomException(500, "Internal Server Error", "Something went wrong");
      }
    }

    [HttpGet]
    public IActionResult GetAllUsers([FromQuery(Name = "start-index")] int startIndex = 0, [FromQuery(Name = "row-size")] int rowSize = 5)
    {
      List<UserDto> users = _userService.GetAllUsers(startIndex, rowSize);
      return Ok(users);
    }
  }
}