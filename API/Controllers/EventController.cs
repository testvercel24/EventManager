using Microsoft.AspNetCore.Mvc;
using Services;
using Entity.Data;
using Entity.Dtos;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
namespace Controller
{
  [Route("api/Event")]
  public class EventController : ControllerBase
  {
    private readonly IConfiguration _config;
    private readonly IEventService _eventService;
    private readonly ILogger<EventController> _logger;
    public EventController(IConfiguration config, IEventService eventService, ILogger<EventController> logger)
    {
      _config = config;
      _eventService = eventService;
      _logger = logger;
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
      _logger.LogInformation("Started creating an event with event name {0}", eventDto.EventName);
      IdDto eventId = _eventService.CreateEvent(eventDto);
      _logger.LogInformation("Successfully created an event with Id {0}", eventId.Id);
      return Ok(eventId);
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
    public IActionResult GetEvents([FromRoute(Name = "event-key"), Required] string eventKey)
    {
      _logger.LogInformation("Getting list of events for {0} key", eventKey);

      List<EventDto> eventDtos = _eventService.GetEvents(eventKey);

      _logger.LogInformation("Successfully fetched the list of event {0}", eventDtos);
      return Ok(eventDtos);
    }


    [HttpPost("{event-id}/user")]
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
      _logger.LogInformation("Started mapping user {0} details to event id {1}", file, eventId);

      List<UserDto>? conflictedUsers = _eventService.CreateAttendee(eventId, file);

      _logger.LogInformation("Successfully mapped {0} to eventId {1} and returns conflicted users {2}", file, eventId, conflictedUsers);
      return Conflict(conflictedUsers);
    }
  }
}