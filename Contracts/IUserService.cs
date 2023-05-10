using Microsoft.AspNetCore.Http;
using Entity.Dtos;
namespace Services
{
  public interface IUserService
  {

    ///<summary>
    ///To upload and map all the details like userIds and userNames ti the UserModel
    ///</summary>
    ///<param name=File>File that contains the details of the userIds and userNames</paam>
    ///<result name=bool>Returns true after succseful upload the details</result>
    bool UploadUser(IFormFile file);

    List<UserDto> GetAllUsers(int startIndex, int rowSize);
  }
}