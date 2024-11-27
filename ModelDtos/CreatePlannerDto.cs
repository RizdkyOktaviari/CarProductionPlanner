using AspnetCoreMvcFull.Models;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.ModelDtos
{
  public class CreatePlannerDto
  {
    [Required(ErrorMessage = "Created By is required")]
    public string CreatedBy { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Week is required")]
    public string Week { get; set; }
    public List<Days>? Days { get; set; }
    public List<PlannerDetailsDto> PlannerDetailsDto { get; set; }
  }
}
