using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.Globalization;
using Entity.Models;
using Repository;
using Entity.Data;
using Microsoft.Extensions.Logging;
using Entity.Dtos;
using AutoMapper;
namespace Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly AutoMapping _autoMapping;
    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
      _userRepository = userRepository;
      _logger = logger;
      _autoMapping = new AutoMapping();
    }

    ///<summary>
    ///To upload and map all the details like userIds and userNames ti the UserModel
    ///</summary>
    ///<param name=File>File that contains the details of the userIds and userNames</paam>
    ///<result name=bool>Returns true after succseful upload the details</result>
    public bool UploadUser(IFormFile file)
    {
      _logger.LogInformation("Started uploading userIds and userNames from {0}", file);
      try
      {
        _logger.LogDebug("Assigning file {0} details to UserModel", file);

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          var users = csv.GetRecords<UserModel>().ToList();
          _logger.LogDebug("Uploding UserModel details {0}", users);
          bool upload = _userRepository.UploadUser(users);
          _logger.LogInformation("Successfully uploaded details users {0}", users);
          return true;
        }
      }
      catch
      {
        _logger.LogError("Invalid csv file bad request");
        throw new CustomException(400, "Bad request", "Invalid Csv file format");
      }
    }
    public List<UserDto> GetAllUsers(int startIndex, int rowSize)
    {
      List<UserModel> userModels = _userRepository.GetAllUsers(startIndex, rowSize);
      var users = _autoMapping.CreateMap < List<UserModel, List<UserDto>>(userModels);
    }
  }
}