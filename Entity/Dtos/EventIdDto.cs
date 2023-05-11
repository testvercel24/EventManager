using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class EventIdDto : EventDto
  {
    [JsonPropertyName("event_id")]
    public Guid Id { get; set; }
  }
}