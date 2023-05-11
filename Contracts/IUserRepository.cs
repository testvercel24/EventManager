using Entity.Models;
using Entity.Dtos;
namespace Repository
{
  public interface IUserRepository
  {

    ///<summary>
    ///To upload the list of userIds and userNames from the file
    ///</summary>
    ///<param name=List<UserModel>>List of all the details of the users like userIds and userNames</param>
    ///<result name=bool>Return true after uploadin the list of all the details of the users</result>
    bool UploadUser(List<UserModel> user);

    ///<summary>
    ///To get all the users from the user repository
    ///</summary>
    ///<param name=startIndex>To get the details from starting of the index value for pagination</param>
    ///<param name=rowSize>to get the required number of row</param>
    ///<result name=List<UserModel>>Returns all the user details for the given range</result>
    List<UserModel> GetAllUsers(int startIndex, int rowSize);

    ///<summary>
    ///To get the particular user details for the given userId
    ///<summary>
    ///<param name=userId>Id of the user to which the details to be fetched</param>
    ///<result name=UserModel>Returns all the details of the user repository with the given Id</result>
    UserModel? GetUserById(int userId);

    ///<summary>
    ///To get all the users associated with the given eventID
    ///</summary>
    ///<param name=eventId>Id of the event for which the users need to be fetched</param>
    ///<result name=List<UserDto>>Returns all the user details associated with given eventId</result>
    List<UserDto> GetUsersForEvent(Guid eventId);
  }
}