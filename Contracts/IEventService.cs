using Entity.Dtos;
using Microsoft.AspNetCore.Http;
namespace Services
{
  public interface IEventService
  {

    ///<summary>
    ///To create a event and map all the event details to entity eventmodel
    ///</summary>
    ///<param name=EventDto> Contains all the details of the event like eventName, startDateTime,endDateTime</param>
    ///<result name=IdDto> Return the Id of the event after succesful creatio of event</result>
    IdDto CreateEvent(EventDto eventDto);


    ///<summary>
    ///To fetch the list of event details for the eventKey
    ///</summary>
    ///<param name=eventKey>Key for which the events should be fetched like all,past,upcoming</param>
    ///<result name=List<EventDto>>
    ///Returns the list of EventDto details such as eventName,startDateTime,endDateTime
    ///</result>
    List<EventDto> GetEvents(string eventKey);


    ///<summary>
    ///To map the list of user details to the event-id
    ///</summary>
    ///<param name=Guid>Id of event for which the attendees should be mapped</param>
    ///<param name=File>List of details of the attendees to be mapped</param>
    ///</result name=UserDto>Return the list of conflicted users</result>
    List<UserDto>? CreateAttendee(Guid eventId, IFormFile file);
  }
}