using Entity.Models;
using Entity.Dtos;
namespace Repository
{
  public interface IEventRepository
  {

    ///<summary>
    /// To upload the Event details to the event table
    ///</summary>
    ///<param name=EventModel>
    ///The entity EventModel contails the details of the event i.e eventName,startDateTime,endDateTime
    ///</param>
    ///<result name=bool>Return true after successfull uploading</result>
    bool CreateEvent(EventModel eventModel);

    ///<summary>
    ///To fetch all the active events from the events table
    ///</summary>
    ///<result name=List<EventDto>>
    ///Returns the list of all the events with eventName,startDateTime,endDateTime
    ///</result>
    List<EventModel> GetAllEvents(int startIndex, int rowSize);

    ///<summary>
    ///To get all the past events that have been completed or started
    ///</summary>
    ///<param name=startIndex>To get the events from startIndex for pagination</param>
    ///<param name=rowSize>To get the required number of rows for pagination</param>
    ///<result name=List<EventModel>Returns the list of all the completed event details</result>
    List<EventModel> GetPastEvents(int startIndex, int rowSize);

    ///<summary>
    ///To get all the upcoming events that have been completed or started
    ///</summary>
    ///<param name=startIndex>To get the events from startIndex for pagination</param>
    ///<param name=rowSize>To get the required number of rows for pagination</param>
    ///<result name=List<EventModel>Returns the list of all the upcoming event details</result>
    List<EventModel> GetUpComingEvents(int startIndex, int rowSize);

    ///<summary>
    ///To Map the event ettendees with userIds and eventIds
    ///</summary>
    ///<param name=List<EventAttendeeModel>>List of EventAttendeeModels mapped with eventIds and userIds</param>
    ///<result name=bool>Returns true if the Mapping details are added successfully</result>
    bool CreateAttendee(List<EventAttendeeModel> eventAttendees);

    ///<summary>
    ///To get the Event details for the given eventId
    ///</summary>
    ///<param name=Guid>Id of the event for which the details should be fetched</param>
    ///<result name=EventModel>Contains all the details eventName,startDateTime,endDateTime</result>
    EventModel? GetEventById(Guid eventId);

    //<summary>
    ///To get the list of conflicted user details within the datetime range of of the event
    ///</summary>
    ///<param name=startDateTime>starting date time of the event</param>
    ///<param name=endDateTime> ending date time of the event</param>
    ///<result name=List<UserDto>>returns the list of conflicted user details</result>
    List<UserDto> GetConflictedUsers(DateTime startDateTime, DateTime endDateTime, List<UserDto> attendees);

    //<summary>
    ///To get all the event details associated for the userId
    ///</summary>
    ///<param name=userId>Id of the user to get all the events associated with</param>
    ///<result name=List<EventDto>>Return list of all the event details associated with userId</result>
    List<EventIdDto> GetEventsForUser(int userId);

  }
}