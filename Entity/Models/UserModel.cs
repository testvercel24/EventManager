using System.ComponentModel.DataAnnotations;
namespace Entity.Models
{
  public class UserModel
  {
    [Key]
    public int UserId { get; set; } = default;
    public string UserName { get; set; } = string.Empty!;
  }
}