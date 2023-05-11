using Entity.Models;
using Entity.Data;
using Entity.Dtos;
namespace Repository
{
  public class EventRepository : IEventRepository
  {
    private readonly ApiDbContext _context;
    public EventRepository(ApiDbContext context)
    {
      _context = context;
    }

    ///<summary>
    /// To upload the Event details to the event table
    ///</summary>
    ///<param name=EventModel>
    ///The entity EventModel contails the details of the event i.e eventName,startDateTime,endDateTime
    ///</param>
    ///<result name=bool>Return true after successfull uploading</result>
    public bool CreateEvent(EventModel eventModel)
    {
      _context.Events.Add(eventModel);
      _context.SaveChanges();
      return true;
    }

    ///<summary>
    ///To fetch all the active events from the events table
    ///</summary>
    ///<result name=List<EventDto>>
    ///Returns the list of all the events with eventName,startDateTime,endDateTime
    ///</result>
    public List<EventModel> GetAllEvents(int startIndex, int rowSize)
    {
      List<EventModel> events = _context.Events.Where(x => x.IsActive == true).Skip(startIndex).Take(rowSize).ToList();
      return events.ToList();
    }


    ///<summary>
    ///To get all the past events that have been completed or started
    ///</summary>
    ///<param name=startIndex>To get the events from startIndex for pagination</param>
    ///<param name=rowSize>To get the required number of rows for pagination</param>
    ///<result name=List<EventModel>Returns the list of all the completed event details</result>
    public List<EventModel> GetPastEvents(int startIndex, int rowSize)
    {
      List<EventModel> events = _context.Events.Where(x => x.IsActive == true && x.StartDateTime < DateTime.Now).Skip(startIndex).Take(rowSize).ToList();
      return events.ToList();
    }

    ///<summary>
    ///To get all the upcoming events that have been completed or started
    ///</summary>
    ///<param name=startIndex>To get the events from startIndex for pagination</param>
    ///<param name=rowSize>To get the required number of rows for pagination</param>
    ///<result name=List<EventModel>Returns the list of all the upcoming event details</result>
    public List<EventModel> GetUpComingEvents(int startIndex, int rowSize)
    {
      List<EventModel> events = _context.Events.Where(x => x.IsActive == true && x.StartDateTime > DateTime.Now).Skip(startIndex).Take(rowSize).ToList();
      return events.ToList();
    }

    ///<summary>
    ///To Map the event ettendees with userIds and eventIds
    ///</summary>
    ///<param name=List<EventAttendeeModel>>List of EventAttendeeModels mapped with eventIds and userIds</param>
    ///<result name=bool>Returns true if the Mapping details are added successfully</result>
    public bool CreateAttendee(List<EventAttendeeModel> eventAttendees)
    {
      _context.AddRange(eventAttendees);
      _context.SaveChanges();
      return true;
    }

    ///<summary>
    ///To get the Event details for the given eventId
    ///</summary>
    ///<param name=Guid>Id of the event for which the details should be fetched</param>
    ///<result name=EventModel>Contains all the details eventName,startDateTime,endDateTime</result>
    public EventModel? GetEventById(Guid eventId)
    {
      return _context.Events.FirstOrDefault(x => x.Id == eventId);
    }

    ///<summary>
    ///To get the list of conflicted user details within the datetime range of of the event
    ///</summary>
    ///<param name=startDateTime>starting date time of the event</param>
    ///<param name=endDateTime> ending date time of the event</param>
    ///<result name=List<UserDto>>returns the list of conflicted user details</result>
    public List<UserDto> GetConflictedUsers(DateTime startDateTime, DateTime endDateTime)
    {
      var users = from events in _context.Events.Where(x => (x.StartDateTime >= startDateTime && x.StartDateTime <= endDateTime)
                                                            || (x.EndDateTime >= startDateTime && x.EndDateTime <= endDateTime))
                  join eventAttendees in _context.EventAttendees
                      on events.Id equals eventAttendees.EventId
                  join user in _context.Users
                      on eventAttendees.UserId equals user.UserId
                  select new UserDto { UserId = user.UserId, UserName = user.UserName };
      return users.ToList();
    }

    ///<summary>
    ///To get all the event details associated for the userId
    ///</summary>
    ///<param name=userId>Id of the user to get all the events associated with</param>
    ///<result name=List<EventDto>>Return list of all the event details associated with userId</result>
    public List<EventIdDto> GetEventsForUser(int userId)
    {
      var events = from eventAttendees in _context.EventAttendees.Where(x => x.UserId == userId)
                   join e in _context.Events
                     on eventAttendees.EventId equals e.Id
                   select new EventIdDto { Id = e.Id, EventName = e.EventName, StartDateTime = e.StartDateTime, EndDateTime = e.EndDateTime };
      return events.ToList();
    }
  }
}