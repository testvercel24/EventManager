using Xunit;
using Entity.Dtos;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Testing
{
  public class EventTesting : BaseTesting
  {
    [Fact]
    public void CreateEventTesting()
    {
      EventDto eventDto = new EventDto()
      {
        // EventName = eventName,
        // StartDateTime = startDateTime,
        // EndDateTime = endDateTime
        EventName = "Integration Session",
        StartDateTime = new DateTime(2023, 05, 10, 11, 00, 00),
        EndDateTime = new DateTime(2023, 05, 10, 11, 30, 00)
      };
      IActionResult result = _eventController.CreateEvent(eventDto);
      var okobject = (OkObjectResult)result;
      IdDto? id = (IdDto?)okobject.Value;
      Assert.NotNull(id);
    }
  }
}