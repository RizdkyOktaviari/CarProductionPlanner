using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Interfaces
{
  public interface IPlannerService
  {
    Task<ResponseDto<Planners>> SubmitPlanner(CreatePlannerDto planners);
    Task<ResponseDto<List<Planners>>> GetPlanners();
    ResponseDto<Planners> GetPlannerById(int id);
  }
}
