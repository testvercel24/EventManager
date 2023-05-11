using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class EventDto
  {
    [JsonPropertyName("event_name")]
    public string EventName { get; set; } = string.Empty!;

    [JsonPropertyName("start_date_time")]
    public DateTime StartDateTime { get; set; } = default;

    [JsonPropertyName("end_date_time")]
    public DateTime EndDateTime { get; set; } = default;
  }
}