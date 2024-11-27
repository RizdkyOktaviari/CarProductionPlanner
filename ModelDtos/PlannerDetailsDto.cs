using AspnetCoreMvcFull.Models;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.ModelDtos
{
  public class PlannerDetailsDto
  {
    public int Id { get; set; }
    public int PlannerId { get; set; }
    [Required(ErrorMessage = "Day Id is required")]
    public int DayId { get; set; }
    [Required(ErrorMessage = "Production Total is required")]
    public int ProductionTotal { get; set; }
  }
}
