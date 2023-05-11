using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class IdDto
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
  }
}