using Microsoft.EntityFrameworkCore;
using Entity.Models;
namespace Entity.Data
{
  public class ApiDbContext : DbContext
  {
    public virtual DbSet<UserModel> Users { get; set; } = default!;
    public virtual DbSet<EventModel> Events { get; set; } = default!;
    public virtual DbSet<EventAttendeeModel> EventAttendees { get; set; } = default!;

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {

    // }
  }
}
