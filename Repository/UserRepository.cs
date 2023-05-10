using Entity.Models;
using Entity.Data;
using Microsoft.Extensions.Logging;

namespace Repository
{

  public class UserRepository : IUserRepository
  {
    private readonly ApiDbContext _context;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(ApiDbContext context, ILogger<UserRepository> logger)
    {
      _context = context;
      _logger = logger;
    }

    ///<summary>
    ///To upload the list of userIds and userNames from the file
    ///</summary>
    ///<param name=List<UserModel>>List of all the details of the users like userIds and userNames</param>
    ///<result name=bool>Return true after uploadin the list of all the details of the users</result>
    public bool UploadUser(List<UserModel> users)
    {
      _logger.LogInformation("Started uploading user details {0}", users);
      List<UserModel> newUsers = (from user in users
                                  join userModel in _context.Users
                                  on user.UserId equals userModel.UserId into newTable
                                  from newRow in newTable.DefaultIfEmpty()
                                  where newRow == null
                                  select new UserModel
                                  {
                                    UserId = user.UserId,
                                    UserName = user.UserName
                                  }).ToList();
      _logger.LogDebug("Fetched the new users {0} from the {1}", newUsers, users);
      _context.Users.AddRange(newUsers);
      _context.SaveChanges();
      _logger.LogInformation("Successfully uploaded the {@users}", users);
      return true;
    }

    public List<UserModel> GetAllUsers(int startIndex, int rowSize)
    {
      List<UserModel> users = _context.Users.Skip(startIndex).Take(rowSize).ToList();
      return users;
    }
  }
}