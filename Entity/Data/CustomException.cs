namespace Entity.Data
{
  public class CustomException : Exception
  {
    public int Code { get; set; } = default!;
    public string Messages { get; set; } = string.Empty!;
    public string Description { get; set; } = string.Empty!;
    public CustomException(int code, string message, string description)
    {
      Code = code;
      Messages = message;
      Description = description;
    }
  }
}