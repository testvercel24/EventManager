using System.Text.Json;
using System.Text.Json.Serialization;
namespace Entity.Dtos
{
  public class ErrorDto
  {
    public int Code { get; set; } = default!;
    public string Message { get; set; } = string.Empty!;
    public string Description { get; set; } = string.Empty!;
    public override String ToString()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}