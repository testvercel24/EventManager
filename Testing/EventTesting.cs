using Xunit;
using Entity.Data;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace Testing
{
  public class EventTesting : BaseTesting
  {
    public EventTesting() : base()
    {

    }

    [Fact]
    public void CreateEventTesting()
    {
      EventDto eventDto = new EventDto()
      {
        EventName = "Integration Session",
        StartDateTime = new DateTime(2023, 05, 14, 11, 00, 00),
        EndDateTime = new DateTime(2023, 05, 14, 11, 30, 00)
      };
      IActionResult result = _eventController2.CreateEvent(eventDto);
      var objectResult = (ObjectResult)result;
      Assert.Equal(201, objectResult.StatusCode);
    }
    [Theory]
    [InlineData("Integration Session", "2024-05-14T11:00:00Z", "2024-05-14T10:30:00Z")]
    [InlineData("Integration Session", "2023-05-11T11:00:00Z", "2023-05-11T11:30:00Z ")]
    public void CreateEvent_BadRequestTesting(string eventName, string startDateTime, string endDateTime)
    {
      EventDto eventDto = new EventDto()
      {
        EventName = eventName,
        StartDateTime = DateTime.Parse(startDateTime),
        EndDateTime = DateTime.Parse(endDateTime)
      };
      var ex = Assert.Throws<CustomException>(() => _eventController2.CreateEvent(eventDto));
      Assert.Equal(400, ex.Code);
    }
    [Theory]
    [InlineData("all")]
    [InlineData("past")]
    [InlineData("upcoming")]
    public void GetEvents(string key)
    {
      // string key = "all";
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
      var result = _eventController2.CreateAttendee(EventId, formFile);
      // var statusCodeResult = Assert.IsType<OkResult>(result);
      // Assert.Equal(200, statusCodeResult.StatusCode);
      var createdResult = (CreatedResult)result;
      Assert.Equal(201, createdResult.StatusCode);
      Assert.Equal("", createdResult.Value);
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