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
      _logger.Info("Started creating an event with event name {0}", eventDto.EventName);

      IdDto eventId = _eventService.CreateEvent(eventDto);

      _logger.Info("Successfully created an event with Id {0}", eventId.Id);

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
      _logger.Info("Getting list of events for {0} key", eventKey);

      List<EventIdDto> eventIdDtos = _eventService.GetEvents(eventKey, startIndex, rowSize);

      _logger.Info("Successfully fetched the list of event {0}", eventIdDtos);

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
      _logger.Info("Started mapping user {0} details to event id {1}", file, eventId);

      List<UserDto> conflictedUsers = _eventService.CreateAttendee(eventId, file);

      _logger.Info("Successfully mapped {0} to eventId {1} and returns conflicted users {2}", file, eventId, conflictedUsers);
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
      _logger.Info("Getting Users for the event with id {0}", eventId);

      List<UserDto> users = _eventService.GetUsersForEvent(eventId);

      _logger.Info("Successfully fetched the users {0} for the event {1}", users, eventId);

      return Ok(users);

    }

  }
}