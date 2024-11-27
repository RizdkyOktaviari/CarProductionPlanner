using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Interfaces
{
  public interface IDayService
  {
    ResponseDto<List<Days>> GettAllDays();
  }
}
