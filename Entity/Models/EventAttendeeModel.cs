namespace Entity.Models
{
  public class EventAttendeeModel : BaseModel
  {
    public int UserId { get; set; } = default;
    public Guid EventId { get; set; } = default;
  }
}