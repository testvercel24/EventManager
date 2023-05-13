using NLog;
using Services;
using Entity.Data;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
namespace Controller
{
  [Route("api/Events")]
  public class EventController : ControllerBase
  {
    private readonly IEventService _eventService;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public EventController(IEventService eventService)
    {
      _eventService = eventService;
    }

    [HttpPost]
    ///<summary>
    ///To create an event within range of datetime
    ///</summary>
    ///<param name=EventDto>Contains the eventName, startDateTime and endDateTime</param>
    ///<result name=IdDto>Id of the vent created</result>
    [SwaggerResponse(201, "Successfull created", typeof(IdDto))]
    [SwaggerResponse(400, "Bad Request", typeof(CustomException))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult CreateEvent([FromBody] EventDto eventDto)
    {
      if (!ModelState.IsValid)
      {
        _logger.Error("Bad request invalid model state");
        throw new CustomException(400, "Bad Request", "Invalid data format");
      }
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Started creating an event with event details", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      IdDto eventId = _eventService.CreateEvent(eventDto);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully created an event with Id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return new ObjectResult(eventId) { StatusCode = 201 };
    }


    [HttpGet("{event-key}")]
    ///<summary>
    ///To get the list of All Event, Past events and Upcoming events.
    ///</summary>
    ///<param name=event-key>Key to get the events as a path param</param>
    ///<result name=List<EventDto>>List of the events with eventNAme,startDateTime,endDateTime</result>
    [SwaggerResponse(200, "Successfully fetched events", typeof(EventDto))]
    [SwaggerResponse(400, "Bad request", typeof(CustomException))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult GetEvents([FromRoute(Name = "event-key"), Required] string eventKey, [FromQuery(Name = "start-index")] int startIndex = 0, [FromQuery(Name = "row-size")] int rowSize = 5)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Getting list of events for key", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      List<EventIdDto> eventIdDtos = _eventService.GetEvents(eventKey, startIndex, rowSize);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully fetched the list of event", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return Ok(eventIdDtos);
    }


    [HttpPost("{event-id}/users")]
    ///<summary>
    ///To map the list of attendees to the event
    ///</summary>
    ///<param name=event-id>Id of the event to which the attendees should be mapped</param>
    ///<param name=File>List of all the userIds and userName as a file</param>
    ///<result name=List<UserDto>>
    ///Returns all the conflicted user details with userId and userName as a list
    ///</result>
    [SwaggerResponse(200, "Successfully mapped", typeof(List<UserDto>))]
    [SwaggerResponse(400, "Bad Request", typeof(CustomException))]
    [SwaggerResponse(404, "Event Not Found", typeof(CustomException))]
    [SwaggerResponse(500, "Internal Server Error", typeof(CustomException))]
    public IActionResult CreateAttendee([FromRoute(Name = "event-id"), Required] Guid eventId, [FromForm(Name = "File"), Required] IFormFile file)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Started mapping user details to event id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      List<UserDto> conflictedUsers = _eventService.CreateAttendee(eventId, file);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully mapped to eventId and returns conflicted users {2}", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);
      if (conflictedUsers.Count != 0)
      {
        return Conflict(conflictedUsers);
      }
      return Created("", "");
    }


    [HttpGet("{event-id}/users")]
    ///<summary>
    ///To get all the users mapped to the particular event
    ///</summary>
    ///<param name="event-id">Id of the event to get the users mapped</param>
    ///<result>List of all the users mapped to the event-id</result>
    [SwaggerResponse(200, "Successfully Fetched all users", typeof(UserDto))]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult GetUsersForEvent([FromRoute(Name = "event-id"), Required] Guid eventId)
    {
      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Getting Users for the event with id", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      List<UserDto> users = _eventService.GetUsersForEvent(eventId);

      _logger.Info("Message: {message},IP address: {ipAddress}, Endpoint: {endpoint}", "Successfully fetched the users for the event", HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path);

      return Ok(users);

    }

  }
}