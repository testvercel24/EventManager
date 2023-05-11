using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class UserDto
  {
    [JsonPropertyName("user_id")]
    public int UserId { get; set; } = default;

    [JsonPropertyName("user_name")]
    public string UserName { get; set; } = string.Empty!;
  }
}