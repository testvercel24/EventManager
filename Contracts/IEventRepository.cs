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
    List<EventDto> GetEvents();

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
    List<UserDto>? GetConflictedUsers(DateTime startDateTime, DateTime endDateTime);

  }
}