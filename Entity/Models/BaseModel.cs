using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entity.Models
{
  public class BaseModel
  {
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
  }
}