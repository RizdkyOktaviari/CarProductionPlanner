using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.ViewModels
{
  public class PlannerDetailViewModel
  {
    public int Id { get; set; }
    public string? CreatedBy { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? Week { get; set; }
    public bool isPlanned { get; set; }
    public List<Days> Days { get; set; }
    public List<PlannerDetails> PlannerDetails { get; set; }
  }
}
