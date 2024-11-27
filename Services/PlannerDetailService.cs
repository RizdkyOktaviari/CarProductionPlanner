using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Services
{
  public class PlannerDetailService : IPlannerDetailService
  {
    private readonly ApplicationDbContext _context;
    public PlannerDetailService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<ResponseDto<List<PlannerDetails>>> GetPlan(int plannerId)
    {
      try
      {
        var plannerDetails = _context.PlannerDetails
            .Where(pd => pd.PlannerId == plannerId)
            .ToList();

        // If no planner details found
        if (plannerDetails.Count == 0)
          return new ResponseDto<List<PlannerDetails>>(false, "No planner details found");

        var daysWithProduction = plannerDetails.Where(pd => pd.ProductionTotal > 0).ToList();

        int totalProduction = daysWithProduction.Sum(pd => pd.ProductionTotal);
        int daysWithProductionCount = daysWithProduction.Count();

        // If no production found
        if (daysWithProductionCount == 0)
          return new ResponseDto<List<PlannerDetails>>(false, "No production found");

        int averageProduction = totalProduction / daysWithProductionCount;

        var updatedPlannerDetails = new List<PlannerDetails>();

        foreach (var detail in plannerDetails)
        {
          // If no production found continue
          if (detail.ProductionTotal == 0)
          {
            updatedPlannerDetails.Add(detail);
            continue;
          }

          // If production is greater than average production, set production to average production + 1
          if (detail.ProductionTotal > averageProduction)
          {
            detail.ProductionTotal = averageProduction + 1;
          }
          else if (detail.ProductionTotal < averageProduction)
          {
            detail.ProductionTotal = averageProduction;
          }

          updatedPlannerDetails.Add(detail);
        }

        return new ResponseDto<List<PlannerDetails>>(true, "Planner details found", updatedPlannerDetails);
      }
      catch (Exception ex)
      {
        return new ResponseDto<List<PlannerDetails>>(false, ex.Message);
      }
    }

    public async Task<ResponseDto<List<PlannerDetails>>> SubmitPlannerDetail(List<PlannerDetails> plannerDetails)
    {
      try
      {
        var plannerDetailsList = new List<PlannerDetails>();
        foreach (var plannerDetail in plannerDetails)
        {
          var plannerDetailEntity = new PlannerDetails
          {
            PlannerId = plannerDetail.PlannerId,
            DayId = plannerDetail.DayId,
            ProductionTotal = plannerDetail.ProductionTotal,
          };
          plannerDetailsList.Add(plannerDetailEntity);
        }
        await _context.PlannerDetails.AddRangeAsync(plannerDetailsList);
        await _context.SaveChangesAsync();
        return new ResponseDto<List<PlannerDetails>>(true, "Planner details created successfully", plannerDetailsList);
      }
      catch (Exception ex)
      {
        return new ResponseDto<List<PlannerDetails>>(false, ex.Message);
      }
    }

    public async Task<ResponseDto<List<PlannerDetails>>> UpdatePlannerDetails(List<PlannerDetails> plannerDetails)
    {
      try
      {
        var plannerDetailsList = new List<PlannerDetails>();
        foreach (var plannerDetail in plannerDetails)
        {
          var plannerDetailEntity = new PlannerDetails
          {
            Id = plannerDetail.Id,
            PlannerId = plannerDetail.PlannerId,
            DayId = plannerDetail.DayId,
            ProductionTotal = plannerDetail.ProductionTotal,
          };
          plannerDetailsList.Add(plannerDetailEntity);
        }
        _context.PlannerDetails.RemoveRange(plannerDetails);
        _context.PlannerDetails.AddRange(plannerDetailsList);
        await _context.SaveChangesAsync();
        return new ResponseDto<List<PlannerDetails>>(true, "Planner details updated successfully", plannerDetailsList);
      }
      catch (Exception ex)
      {
        return new ResponseDto<List<PlannerDetails>>(false, ex.Message);
      }
    }
  }
}
