using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Entity.Dtos
{
  public class EventDto
  {
    [JsonPropertyName("event_name")]
    [Required]
    public string EventName { get; set; } = string.Empty!;

    [JsonPropertyName("start_date_time")]
    [Required]
    public DateTime StartDateTime { get; set; } = default;

    [JsonPropertyName("end_date_time")]
    [Required]
    public DateTime EndDateTime { get; set; } = default;
  }
}