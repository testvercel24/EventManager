using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entity.Models
{
  public class UserModel
  {
    public UserModel()
    {
      eventAttendee = new HashSet<EventAttendeeModel>();
    }
    [Key]
    [Column("user_id")]
    public int UserId { get; set; } = default;

    [Column("user_name")]
    public string UserName { get; set; } = string.Empty!;
    public ICollection<EventAttendeeModel> eventAttendee { get; set; }
  }
}