using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using AspnetCoreMvcFull.ModelDtos;
using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.ViewModels;
using System.Runtime.CompilerServices;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
  private readonly IPlannerService _plannerService;
  private readonly IDayService _dayService;
  private readonly IPlannerDetailService _plannerDetailService;
  private readonly ITransactionManagementService _transactionManagementService;
  public DashboardsController(IPlannerService plannerService, IDayService dayService, IPlannerDetailService plannerDetailService, ITransactionManagementService transactionManagementService)
  {
    _plannerService = plannerService;
    _dayService = dayService;
    _plannerDetailService = plannerDetailService;
    _transactionManagementService = transactionManagementService;
  }

  private CreatePlannerDto InitCreatePlannerDto(CreatePlannerDto model)
  {
    var days = _dayService.GettAllDays();
    model.Days = days.Data.Select(d => new Days
    {
      Id = d.Id,
      Name = d.Name
    }).ToList();
    return model;
  }

  private PlannerDetailViewModel InitPlannerDetailViewModel(int id, PlannerDetailViewModel model, bool isPlanned)
  {
    var planner = _plannerService.GetPlannerById(id);
    var days = _dayService.GettAllDays();
    var plannerDetails = new List<PlannerDetails>();
    if (!planner.Success)
    {
      return null;
    }
    if (isPlanned)
    {
      var response = _plannerDetailService.GetPlan(id);
      if (response.Result.Success)
      {
        plannerDetails = response.Result.Data;
      }
    }
    else
    {
      plannerDetails = planner.Data.PlannerDetails.ToList();
    }
    model.Id = planner.Data.Id;
    model.CreatedBy = planner.Data.CreatedBy;
    model.Email = planner.Data.Email;
    model.Description = planner.Data.Description;
    model.Week = planner.Data.Week;
    model.PlannerDetails = plannerDetails.Select(p => new PlannerDetails
    {
      Id = p.Id,
      PlannerId = p.PlannerId,
      DayId = p.DayId,
      ProductionTotal = p.ProductionTotal,
      Days = new Days
      {
        Id = p.Days.Id,
        Name = p.Days.Name
      }
    }).ToList();
    model.Days = days.Data;
    model.isPlanned = isPlanned;
    return model;
  }

  public IActionResult Index()
  {
    ViewData["Title"] = "Planner";
    var plannersResponse = _plannerService.GetPlanners();
    if (!plannersResponse.Result.Success)
    {
      NotFound(plannersResponse.Result.Message);
    }
    var planners = plannersResponse.Result.Data;

    var plannerViewModels = planners.Select(p => new PlannerViewModel
    {
      Id = p.Id,
      CreatedBy = p.CreatedBy,
      Description = p.Description,
      Week = p.Week
    }).ToList();

    return View("~/Views/Dashboards/CreatePlanner/index.cshtml", plannerViewModels);
  }

  public IActionResult CreatePlanner()
  {
    ViewData["Title"] = "Create Planner";
    var model = InitCreatePlannerDto(new CreatePlannerDto());
    return View("~/Views/Dashboards/CreatePlanner/create.cshtml", model);
  }

  [HttpGet("DetailPlanner/{id}/{isPlanned}")]
  public IActionResult DetailPlanner([FromRoute]int id, [FromRoute]bool isPlanned)
  {
    ViewData["Title"] = "Detail Planner";
    var model = InitPlannerDetailViewModel(id, new PlannerDetailViewModel(), isPlanned);
    if (model == null)
    {
      return NotFound();
    }
    return View("~/Views/Dashboards/CreatePlanner/detail.cshtml", model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SubmitPlanner([FromForm] CreatePlannerDto model)
  {
    if (!ModelState.IsValid)
    {
      var modelreturned = InitCreatePlannerDto(model);
      return View("~/Views/Dashboards/CreatePlanner/create.cshtml", modelreturned);
    }

    var transaction = await _transactionManagementService.BeginTransactionAsync();
    try
    {
      var planner = new Planners
      {
        CreatedBy = model.CreatedBy,
        Email = model.Email,
        Description = model.Description,
        Week = model.Week
      };

      var responsePlanner = _plannerService.SubmitPlanner(model);
      if (!responsePlanner.Result.Success)
      {
        return BadRequest(responsePlanner.Result.Message);
      }

      var plannerDetails = model.PlannerDetailsDto.Select(p => new PlannerDetails
      {
        PlannerId = responsePlanner.Result.Data.Id,
        DayId = p.DayId,
        ProductionTotal = p.ProductionTotal,
      }).ToList();

      var responsePlannerDetails = _plannerDetailService.SubmitPlannerDetail(plannerDetails);
      if (!responsePlannerDetails.Result.Success)
      {
        return BadRequest(responsePlannerDetails.Result.Message);
      }

      await _transactionManagementService.CommitTransactionAsync(transaction);
      return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
      await _transactionManagementService.RollbackTransactionAsync(transaction);
      var modelreturned = InitCreatePlannerDto(model);
      return View("~/Views/Dashboards/CreatePlanner/create.cshtml", modelreturned);
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SavePlannerDetail([FromForm]int id)
  {
    var transaction = await _transactionManagementService.BeginTransactionAsync();
    try
    {
      var settedPlannerDetail = _plannerDetailService.GetPlan(id);
      if (!settedPlannerDetail.Result.Success)
      {
        return BadRequest(settedPlannerDetail.Result.Message);
      }
      var responseUpdate = _plannerDetailService.UpdatePlannerDetails(settedPlannerDetail.Result.Data);
      if (!responseUpdate.Result.Success)
      {
        return BadRequest(responseUpdate.Result.Message);
      }
      await _transactionManagementService.CommitTransactionAsync(transaction);
      return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
      await _transactionManagementService.RollbackTransactionAsync(transaction);
      var modelreturned = InitPlannerDetailViewModel(id, new PlannerDetailViewModel(), false);
      return View("~/Views/Dashboards/CreatePlanner/detail.cshtml", modelreturned);
    }
  }
}


