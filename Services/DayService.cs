using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Services
{
  public class DayService : IDayService
  {
    private readonly ApplicationDbContext _applicationDbContext;
    public DayService(ApplicationDbContext applicationDbContext)
    {
      _applicationDbContext = applicationDbContext;
    }
    public ResponseDto<List<Days>> GettAllDays()
    {
      try
      {
        var days = _applicationDbContext.Days.ToList();
        return new ResponseDto<List<Days>>(true, "Days retrieved successfully", days);
      }
      catch (Exception ex)
      {
        return new ResponseDto<List<Days>>(false, ex.Message);
      }
    }
  }
}
