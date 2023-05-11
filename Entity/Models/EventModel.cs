using System.ComponentModel.DataAnnotations.Schema;
namespace Entity.Models
{
  public class EventModel : BaseModel
  {
    public EventModel()
    {
      eventAttendeeModels = new HashSet<EventAttendeeModel>();
    }
    [Column("event_name")]
    public string EventName { get; set; } = string.Empty!;

    [Column("start_date_time")]
    public DateTime StartDateTime { get; set; } = default;

    [Column("end_date_time")]
    public DateTime EndDateTime { get; set; } = default;
    public ICollection<EventAttendeeModel> eventAttendeeModels { get; set; }
  }
}