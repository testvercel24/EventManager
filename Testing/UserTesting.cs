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
  public class UserTesting : BaseTesting
  {
    [Fact]
    public void UploadUser()
    {
      string csvContent = "UserId,UserName\n3,John\n4,Jane";
      string filePath = "file.csv";
      File.WriteAllText(filePath, csvContent);
      // Convert the CSV file into a byte array
      byte[] fileBytes = File.ReadAllBytes(filePath);
      // Create an IFormFile instance using the byte array
      IFormFile formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.csv");
      var result = _userController.UploadUser(formFile);
      var statusCodeResult = Assert.IsType<OkResult>(result);
      Assert.Equal(200, statusCodeResult.StatusCode);
    }
    [Fact]
    public void GetAllUsers()
    {
      IActionResult result = _userController.GetAllUsers();
      var okObjectResult = Assert.IsType<OkObjectResult>(result);
      var returnedUsers = Assert.IsAssignableFrom<List<UserDto>>(okObjectResult.Value);
      Assert.NotNull(returnedUsers);
      Assert.NotEmpty(returnedUsers);
    }
    [Fact]
    public void GetUserById()
    {
      IActionResult result = _userController.GetUserById(1);
      var okObjectResult = Assert.IsType<OkObjectResult>(result);
      var returnedUser = Assert.IsAssignableFrom<UserDto>(okObjectResult.Value);
      Assert.Equal(1, returnedUser.UserId);
    }
    [Fact]
    public void GetUserById_NotFound()
    {
      var ex = Assert.Throws<CustomException>(() => _userController.GetUserById(3));
      Assert.Equal(404, ex.Code);
    }
    [Fact]
    public void GetEventsForUser()
    {
      IActionResult result = _userController.GetEventsForUser(1);
      var okObjectResult = Assert.IsType<OkObjectResult>(result);
      var returnedEvents = Assert.IsAssignableFrom<List<EventIdDto>>(okObjectResult.Value);
      Assert.Equal(EventId, returnedEvents[0].Id);
    }
  }
}