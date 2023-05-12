using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class EventIdDto : EventDto
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
  }
}