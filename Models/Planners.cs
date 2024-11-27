using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models
{
  [Table("planners")]
  public class Planners
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("created_by")]
    public string CreatedBy { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("week")]
    public string Week { get; set; }
    public ICollection<PlannerDetails> PlannerDetails { get; set; }
  }
}
