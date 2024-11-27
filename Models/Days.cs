using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models
{
  [Table("days")]
  public class Days
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    public ICollection<PlannerDetails> PlannerDetails { get; set; }
  }
}
