namespace Entity.Dtos
{
  public class EventDto
  {
    public string EventName { get; set; } = string.Empty!;
    public DateTime StartDateTime { get; set; } = default;
    public DateTime EndDateTime { get; set; } = default;
  }
}