namespace Entity.Models
{
  public class BaseModel
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = default;
    public bool IsActive { get; set; } = default;
  }
}