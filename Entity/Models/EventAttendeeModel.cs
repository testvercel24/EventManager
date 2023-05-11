using System.ComponentModel.DataAnnotations.Schema;
namespace Entity.Models
{
  public class EventAttendeeModel : BaseModel
  {
    [Column("user_id")]
    [ForeignKey("UserModel")]
    public int UserId { get; set; } = default;

    [Column("event_id")]
    [ForeignKey("EventModel")]
    public Guid EventId { get; set; } = default;

  }
}