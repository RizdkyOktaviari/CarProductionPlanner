using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models
{
  [Table("planner_details")]
  public class PlannerDetails
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("planner_id")]
    public int PlannerId { get; set; }
    [Column("day_id")]
    public int DayId { get; set; }
    [Column("production_total")]
    public int ProductionTotal { get; set; }
    public Planners Planners { get; set; }
    public Days Days { get; set; }
  }
}
