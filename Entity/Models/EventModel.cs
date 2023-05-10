namespace Entity.Models
{
  public class EventModel : BaseModel
  {
    public string EventName { get; set; } = string.Empty!;
    public DateTime StartDateTime { get; set; } = default;
    public DateTime EndDateTime { get; set; } = default;
  }
}