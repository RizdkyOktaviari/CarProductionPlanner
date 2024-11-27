using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Interfaces
{
  public interface IPlannerDetailService
  {
    Task<ResponseDto<List<PlannerDetails>>> SubmitPlannerDetail(List<PlannerDetails> plannerDetails);
    Task<ResponseDto<List<PlannerDetails>>> GetPlan(int plannerId);
    Task<ResponseDto<List<PlannerDetails>>> UpdatePlannerDetails(List<PlannerDetails> plannerDetails);
  }
}
