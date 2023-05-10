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

    List<UserModel> GetAllUsers(int startIndex, int rowSize);
  }
}