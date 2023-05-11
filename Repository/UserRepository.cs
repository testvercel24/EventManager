using Entity.Models;
using Entity.Data;
using Microsoft.Extensions.Logging;
using Entity.Dtos;
using NLog;
namespace Repository
{

  public class UserRepository : IUserRepository
  {
    private readonly ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public UserRepository(ApiDbContext context)
    {
      _context = context;
    }

    ///<summary>
    ///To upload the list of userIds and userNames from the file
    ///</summary>
    ///<param name=List<UserModel>>List of all the details of the users like userIds and userNames</param>
    ///<result name=bool>Return true after uploadin the list of all the details of the users</result>
    public bool UploadUser(List<UserModel> users)
    {
      _logger.Info("Started uploading user details {0}", users);

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
      _logger.Debug("Fetched the new users {0} from the {1}", newUsers, users);

      _context.Users.AddRange(newUsers);
      _context.SaveChanges();

      _logger.Info("Successfully uploaded the {@users}", users);
      return true;
    }

    ///<summary>
    ///To get all the users from the user repository
    ///</summary>
    ///<param name=startIndex>To get the details from starting of the index value for pagination</param>
    ///<param name=rowSize>to get the required number of row</param>
    ///<result name=List<UserModel>>Returns all the user details for the given range</result>
    public List<UserModel> GetAllUsers(int startIndex, int rowSize)
    {
      List<UserModel> users = _context.Users.Skip(startIndex).Take(rowSize).ToList();
      return users;
    }

    ///<summary>
    ///To get the particular user details for the given userId
    ///<summary>
    ///<param name=userId>Id of the user to which the details to be fetched</param>
    ///<result name=UserModel>Returns all the details of the user repository with the given Id</result>
    public UserModel? GetUserById(int userId)
    {
      _logger.Info("Getting user repository details for {0}", userId);
      UserModel? user = _context.Users.FirstOrDefault(u => u.UserId == userId);
      _logger.Info("Successfully fetched user repository details {0}", user);
      return user;
    }

    ///<summary>
    ///To get all the users associated with the given eventID
    ///</summary>
    ///<param name=eventId>Id of the event for which the users need to be fetched</param>
    ///<result name=List<UserDto>>Returns all the user details associated with given eventId</result>
    public List<UserDto> GetUsersForEvent(Guid eventId)
    {
      _logger.Info("Fetching all the users with event Id  {0}", eventId);
      var users = from eventAttendees in _context.EventAttendees.Where(x => x.EventId == eventId)
                  join user in _context.Users
                      on eventAttendees.UserId equals user.UserId
                  select new UserDto { UserId = user.UserId, UserName = user.UserName };
      _logger.Info("Successfully fetched all the users for the event with Id {0}", eventId);
      return users.ToList();
    }

  }
}