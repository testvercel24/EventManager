using Xunit;
using Entity.Dtos;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Entity.Data;
using Xunit.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Http;
namespace Testing
{
  public class EventTesting : BaseTesting
  {
    [Fact]
    public void CreateEventTesting()
    {
      EventDto eventDto = new EventDto()
      {
        EventName = "Integration Session",
        StartDateTime = new DateTime(2023, 05, 14, 11, 00, 00),
        EndDateTime = new DateTime(2023, 05, 14, 11, 30, 00)
      };
      IActionResult result = _eventController.CreateEvent(eventDto);
      var okobject = (OkObjectResult)result;
      IdDto? id = (IdDto?)okobject.Value;
      Assert.NotNull(id);
    }
    [Theory]
    [InlineData("Integration Session", "2023-05-14T11:00:00Z", "2023-05-14T10:30:00Z")]
    public void CreateEvent_BadRequestTesting(string eventName, string startDateTime, string endDateTime)
    {
      EventDto eventDto = new EventDto()
      {
        EventName = eventName,
        StartDateTime = DateTime.Parse(startDateTime),
        EndDateTime = DateTime.Parse(endDateTime)
      };
      var ex = Assert.Throws<CustomException>(() => _eventController.CreateEvent(eventDto));
      Assert.Equal(400, ex.Code);
    }
    [Fact]
    public void GetEvents()
    {
      string key = "all";
      IActionResult result = _eventController.GetEvents(key);
      var okObjectResult = Assert.IsType<OkObjectResult>(result);
      var returnedEvents = Assert.IsAssignableFrom<List<EventIdDto>>(okObjectResult.Value);
      Assert.NotNull(returnedEvents);
      Assert.NotEmpty(returnedEvents);
    }
    [Fact]
    public void CreateAttendee()
    {
      string csvContent = "UserId,UserName\n3,John\n4,Jane";
      string filePath = "file.csv";
      File.WriteAllText(filePath, csvContent);
      // Convert the CSV file into a byte array
      byte[] fileBytes = File.ReadAllBytes(filePath);
      // Create an IFormFile instance using the byte array
      IFormFile formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.csv");
      var result = _eventController.CreateAttendee(EventId, formFile);
      var statusCodeResult = Assert.IsType<OkResult>(result);
      Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public void GetUsersForEvent()
    {
      IActionResult result = _eventController.GetUsersForEvent(EventId);
      var okObjectResult = Assert.IsType<OkObjectResult>(result);
      var returnedEvents = Assert.IsAssignableFrom<List<UserDto>>(okObjectResult.Value);
      Assert.NotNull(returnedEvents);
      Assert.NotEmpty(returnedEvents);
    }
  }
}