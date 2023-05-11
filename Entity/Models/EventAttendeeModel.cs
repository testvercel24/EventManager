using System.ComponentModel.DataAnnotations.Schema;
namespace Entity.Models
{
  public class EventAttendeeModel : BaseModel
  {
    [Column("user_id")]
    [ForeignKey("userModel")]
    public int UserId { get; set; } = default;

    [Column("event_id")]
    [ForeignKey("eventModel")]
    public Guid EventId { get; set; } = default;
    public virtual EventModel? eventModel { get; set; }
    public virtual UserModel? userModel { get; set; }

  }
}