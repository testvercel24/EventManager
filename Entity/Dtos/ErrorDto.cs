using System.Text.Json;
using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
  public class ErrorDto
  {
    [JsonPropertyName("code")]
    public int Code { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty!;

    public override String ToString()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}