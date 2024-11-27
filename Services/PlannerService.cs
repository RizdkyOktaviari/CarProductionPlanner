using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AspnetCoreMvcFull.Services
{
  public class PlannerService : IPlannerService
  {
    private readonly ApplicationDbContext _context;

    public PlannerService(ApplicationDbContext context)
    {
      _context = context;
    }

    public ResponseDto<Planners> GetPlannerById(int id)
    {
      try
      {
        var planner = _context.Planners
          .Include(p => p.PlannerDetails)
          .Include(p => p.PlannerDetails)
            .ThenInclude(p => p.Days)
          .FirstOrDefault(p => p.Id == id);
        if (planner == null)
        {
          throw new Exception("Planner not found");
        }
        return new ResponseDto<Planners>(true, "Planner retrieved successfully", planner);
      }
      catch (Exception ex)
      {
        return new ResponseDto<Planners>(false, ex.Message);
      }
    }

    public async Task<ResponseDto<List<Planners>>> GetPlanners()
    {
      try
      {
        var planners = await _context.Planners.ToListAsync();
        return new ResponseDto<List<Planners>>(true, "Planners retrieved successfully", planners);
      }
      catch (Exception ex)
      {
        return new ResponseDto<List<Planners>>(false, ex.Message);
      }
    }

    public async Task<ResponseDto<Planners>> SubmitPlanner(CreatePlannerDto model)
    {
      try
      {
        var planner = new Planners
        {
          CreatedBy = model.CreatedBy,
          Email = model.Email,
          Description = model.Description,
          Week = model.Week
        };

        _context.Planners.Add(planner);
        await _context.SaveChangesAsync();

        return new ResponseDto<Planners>(true, "Planner created successfully", planner);
      }
      catch (Exception ex)
      {
        return new ResponseDto<Planners>(false, ex.Message);
      }
    }
  }
}
