using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
  public DbSet<Days> Days { get; set; }
  public DbSet<Planners> Planners { get; set; }
  public DbSet<PlannerDetails> PlannerDetails { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Planners>()
      .HasMany(p => p.PlannerDetails)
      .WithOne(d => d.Planners)
      .HasForeignKey(d => d.PlannerId);

    modelBuilder.Entity<Days>()
      .HasMany(d => d.PlannerDetails)
      .WithOne(p => p.Days)
      .HasForeignKey(p => p.DayId);

    modelBuilder.Entity<Days>().HasData(
      new Days { Id = 1, Name = "Monday" },
      new Days { Id = 2, Name = "Tuesday" },
      new Days { Id = 3, Name = "Wednesday" },
      new Days { Id = 4, Name = "Thursday" },
      new Days { Id = 5, Name = "Friday" },
      new Days { Id = 6, Name = "Saturday" },
      new Days { Id = 7, Name = "Sunday" }
    );
  }
}
