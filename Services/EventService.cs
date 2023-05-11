using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.Globalization;
using Entity.Models;
using Repository;
using Entity.Data;
using Entity.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Services
{
  public class EventService : IEventService
  {
    private readonly IEventRepository _eventRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<EventService> _logger;
    public EventService(IEventRepository eventRepository, IUserService userService,
                        IUserRepository userRepository, IMapper mapper, ILogger<EventService> logger)
    {
      _eventRepository = eventRepository;
      _userService = userService;
      _mapper = mapper;
      _logger = logger;
      _userRepository = userRepository;
    }

    ///<summary>
    ///To create a event and map all the event details to entity eventmodel
    ///</summary>
    ///<param name=EventDto>
    ///Contains all the details of the event like eventName, startDateTime,endDateTime
    ///</param>
    ///<result name=IdDto> Return the Id of the event after succesful creatio of event</result>
    public IdDto CreateEvent(EventDto eventDto)
    {
      _logger.LogInformation("Started creating an event with event details {0}", eventDto);
      if (eventDto.StartDateTime >= eventDto.EndDateTime)
      {
        _logger.LogError("Bad request where event start time is beyond the end time");
        throw new CustomException(400, "Bad Request", "Event start time should be before end time");
      }
      else if (DateTime.Now > eventDto.StartDateTime)
      {
        _logger.LogError("Bad Request where start data time should be upcoming event");
        throw new CustomException(400, "Bad Request", "Event cannot be started in past");
      }
      _logger.LogDebug("Mapping event dto {0} to event model", eventDto);
      EventModel eventModel = _mapper.Map<EventModel>(eventDto);
      _logger.LogDebug("Uploading the {0} details to database", eventModel);
      bool createdEvent = _eventRepository.CreateEvent(eventModel);
      if (createdEvent == false)
      {
        _logger.LogError("Internal server error event not creted successfully");
        throw new CustomException(500, "Internal Server Error", "Something went wrong");
      }
      _logger.LogInformation("Event created successfully with id {0}", eventModel.Id);
      return new IdDto()
      {
        Id = eventModel.Id
      };
    }

    ///<summary>
    ///To fetch the list of event details for the eventKey
    ///</summary>
    ///<param name=eventKey>Key for which the events should be fetched like all,past,upcoming</param>
    ///<result name=List<EventDto>>
    ///Returns the list of EventDto details such as eventName,startDateTime,endDateTime
    ///</result>
    public List<EventIdDto> GetEvents(string eventKey, int startIndex, int rowSize)
    {
      _logger.LogInformation("Getting events for the key {0}", eventKey);
      if (eventKey.ToLower() == "all")
      {
        List<EventModel> eventModels = _eventRepository.GetAllEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.LogInformation("Succesfully fetched all the ecvnts for {0} returning {1}", eventKey, events);
        return events;
      }
      else if (eventKey.ToLower() == "past")
      {
        List<EventModel> eventModels = _eventRepository.GetPastEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.LogInformation("Successfully fetched all the events for {0} returning {1}", eventKey, events);
        return events;
      }
      else if (eventKey.ToLower() == "upcoming")
      {
        List<EventModel> eventModels = _eventRepository.GetUpComingEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.LogInformation("Successfully fetched all the events for {0} returning {1}", eventKey, events);
        return events;
      }
      else
      {
        _logger.LogError("Bad request key {0} is invalid", eventKey);
        throw new CustomException(400, "Bad Request", "Invalid event key");
      }
    }

    ///<summary>
    ///To map the list of user details to the event-id
    ///</summary>
    ///<param name=Guid>Id of event for which the attendees should be mapped</param>
    ///<param name=File>List of details of the attendees to be mapped</param>
    ///</result name=UserDto>Return the list of conflicted users</result>
    public List<UserDto> CreateAttendee(Guid eventId, IFormFile file)
    {
      _logger.LogInformation("Sarted mapping {0} to event with id {1}", file, eventId);

      _userService.UploadUser(file);

      _logger.LogDebug("Getting event detaild for id {0}", eventId);

      EventModel? eventModel = _eventRepository.GetEventById(eventId);
      if (eventModel == null || eventModel.IsActive == false)
      {
        _logger.LogError("Event with id {0} is not found", eventId);
        throw new CustomException(404, "Not Found", "Event not found");
      }

      _logger.LogDebug("Getting the list of conflicted user for the datetime");
      List<UserDto> conflictedUsers = _eventRepository.GetConflictedUsers(eventModel.StartDateTime, eventModel.EndDateTime);
      //Getting list of conflicted user ids
      List<int> conflictedUserIds = conflictedUsers.Select(x => x.UserId).ToList();
      using (var reader = new StreamReader(file.OpenReadStream()))
      using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
      {
        var users = csv.GetRecords<UserDto>().Where(x => conflictedUserIds.Contains(x.UserId) == false);
        //mapping configuration for mapping userId to the EventAttendeeModel
        var config = new MapperConfiguration(cfg =>
        {
          cfg.CreateMap<int, EventAttendeeModel>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src))
          .ForMember(dest => dest.EventId, opt => opt.Ignore());
        });
        var mapper = new Mapper(config);
        List<EventAttendeeModel> eventAttendees = mapper.Map<List<int>, List<EventAttendeeModel>>(users.Select(x => x.UserId).ToList());
        eventAttendees.ForEach(x => x.EventId = eventId);
        _logger.LogDebug("Uploading eventAttendeeModel details {0} to eventAttendee table", eventAttendees);
        _eventRepository.CreateAttendee(eventAttendees);
      }
      _logger.LogInformation("Successfully mapped the users {0} to event {1}", file, eventId);
      return conflictedUsers;
    }

    ///<summary>
    ///To get all the users mapped to the particular event
    ///</summary>
    ///<param name=eventId>Id of the event to get the users mapped</param>
    ///<result name=List<UserDto>>Returns list of all the users mapped to the event-id</result>
    public List<UserDto> GetUsersForEvent(Guid eventId)
    {
      List<UserDto> users = _userRepository.GetUsersForEvent(eventId);
      return users;
    }
  }
}