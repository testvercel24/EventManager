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
  public class EventService : IEventService
  {
    private readonly IEventRepository _eventRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public EventService(IEventRepository eventRepository, IUserService userService,
                        IUserRepository userRepository, IMapper mapper)
    {
      _eventRepository = eventRepository;
      _userService = userService;
      _mapper = mapper;
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
      _logger.Info("Started creating an event with event details {0}", eventDto);
      if (eventDto.StartDateTime >= eventDto.EndDateTime)
      {
        _logger.Error("Bad request where event start time is beyond the end time");
        throw new CustomException(400, "Bad Request", "Event start time should be before end time");
      }
      else if (DateTime.Now > eventDto.StartDateTime)
      {
        _logger.Error("Bad Request where start data time should be upcoming event");
        throw new CustomException(400, "Bad Request", "Event cannot be started in past");
      }
      _logger.Debug("Mapping event dto {0} to event model", eventDto);
      EventModel eventModel = _mapper.Map<EventModel>(eventDto);
      _logger.Debug("Uploading the {0} details to database", eventModel);
      bool createdEvent = _eventRepository.CreateEvent(eventModel);
      if (createdEvent == false)
      {
        _logger.Error("Internal server error event not creted successfully");
        throw new CustomException(500, "Internal Server Error", "Something went wrong");
      }
      _logger.Info("Event created successfully with id {0}", eventModel.Id);
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
      _logger.Info("Getting events for the key {0}", eventKey);
      if (eventKey.ToLower() == "all")
      {
        List<EventModel> eventModels = _eventRepository.GetAllEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.Info("Succesfully fetched all the ecvnts for {0} returning {1}", eventKey, events);
        return events;
      }
      else if (eventKey.ToLower() == "past")
      {
        List<EventModel> eventModels = _eventRepository.GetPastEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.Info("Successfully fetched all the events for {0} returning {1}", eventKey, events);
        return events;
      }
      else if (eventKey.ToLower() == "upcoming")
      {
        List<EventModel> eventModels = _eventRepository.GetUpComingEvents(startIndex, rowSize);
        List<EventIdDto> events = _mapper.Map<List<EventIdDto>>(eventModels);
        _logger.Info("Successfully fetched all the events for {0} returning {1}", eventKey, events);
        return events;
      }
      else
      {
        _logger.Error("Bad request key {0} is invalid", eventKey);
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
      _logger.Info("Sarted mapping {0} to event with id {1}", file, eventId);

      _logger.Debug("Getting event detaild for id {0}", eventId);
      EventModel? eventModel = _eventRepository.GetEventById(eventId);
      if (eventModel == null || eventModel.IsActive == false || eventModel.StartDateTime < DateTime.Now || eventModel.EndDateTime < DateTime.Now)
      {
        _logger.Error("Event with id {0} is not found", eventId);
        throw new CustomException(404, "Not Found", "Event not found");
      }
      //Uploading new users to the user repository
      _userService.UploadUser(file);

      using (var reader = new StreamReader(file.OpenReadStream()))
      using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
      {
        _logger.Debug("Getting all the records from file");
        List<UserDto> users = csv.GetRecords<UserDto>().ToList();

        _logger.Debug("Getting the list of conflicted user for the datetime");
        List<UserDto> conflictedUsers = GetConflictedUsers(eventModel.StartDateTime, eventModel.EndDateTime, users);
        //Gettings users other than conflicted users
        users = users.Except(conflictedUsers).ToList();
        //Mapping UserId and EventId details to EventAttendeeModel
        List<EventAttendeeModel> eventAttendees = _mapper.Map<List<EventAttendeeModel>>(users.Select(x => x.UserId).ToList());
        eventAttendees.ForEach(x => x.EventId = eventId);

        _logger.Debug("Uploading eventAttendeeModel details {0} to eventAttendee table", eventAttendees);
        _eventRepository.CreateAttendee(eventAttendees);

        _logger.Info("Successfully mapped the users {0} to event {1}", file, eventId);

        return conflictedUsers;
      }
    }

    ///<summary>
    ///To get all the users mapped to the particular event
    ///</summary>
    ///<param name=eventId>Id of the event to get the users mapped</param>
    ///<result name=List<UserDto>>Returns list of all the users mapped to the event-id</result>
    public List<UserDto> GetUsersForEvent(Guid eventId)
    {
      _logger.Info("Getting all the users for the evet with ID {0}", eventId);
      //Getting event details
      EventModel? eventModel = _eventRepository.GetEventById(eventId);
      if (eventModel == null)
      {
        _logger.Error("No event found with the Id {0}", eventId);
        throw new CustomException(404, "Not found", "Event not found");
      }

      List<UserDto> users = _userRepository.GetUsersForEvent(eventId);

      _logger.Info("Successfully fetched all the users for the event with Id {0}", eventId);
      return users;
    }

    public List<UserDto> GetConflictedUsers(DateTime startDateTime, DateTime endDateTime, List<UserDto> users)
    {
      _logger.Info("Getting the conflicted users for range of datetime {0} and {1}", startDateTime, endDateTime);

      List<UserDto> allConflictedUsers = _eventRepository.GetConflictedUsers(startDateTime, endDateTime, users);
      //Getting the conflicted users that are common in users and allconflictedUsers
      var conflictedUsers = from user1 in users
                            join user2 in allConflictedUsers
                            on new { user1.UserId, user1.UserName } equals new { user2.UserId, user2.UserName }
                            select user1;

      _logger.Info("Returning the list of conflicted user {0} among {1}", conflictedUsers, users);
      return conflictedUsers.ToList();
    }
  }
}