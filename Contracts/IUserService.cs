using Entity.Dtos;
using Microsoft.AspNetCore.Http;
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

    ///<summary>
    ///To get all the active users available
    ///</summary>
    ///<param name=startIndex>To start the index for pagination</param>
    ///<param name=rowSize>Number of rows to be returned</param>
    ///<result name=List<UserDto>>Returns all the list of users details</result>
    List<UserDto> GetAllUsers(int startIndex, int rowSize);

    ///<summary>
    ///To get user details for particular user-id
    ///</summary>
    ///<param name=userId>Id of the user for which the details to be fetched</param>
    ///<result name=UserDto>Returns all details of the user with userId</result>
    UserDto GetUserById(int userId);

    ///<summary>
    ///To get all the events for the given userId
    ///</summary>
    ///<param name=userId>Id to get all the associated event details</param>
    ///<result name=List<EventIdDto>Returns all the details of the events associated with userId</result>
    List<EventIdDto> GetEventsForUser(int userId);
  }
}