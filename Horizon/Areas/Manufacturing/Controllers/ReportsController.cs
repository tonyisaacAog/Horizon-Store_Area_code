using Horizon.Areas.Manufacturing.Services;
using Horizon.Areas.Manufacturing.VewModels.reports;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Manufacturing.Controllers;

[Area("Manufacturing")]
public class ReportsController : Controller
{
    private readonly ReportManager _reportManager;

    public ReportsController(ReportManager reportManager)
    {
        _reportManager = reportManager;
    }
    public IActionResult ManufacturingReport()
    {
        var reportVM = new Report();
        return View(reportVM);
    }

    [ModelValidationWithJsonFeedBackFilter]
    public async Task<JsonResult> LoadReport([FromBody] SearchDateVM searchDateVM)
    {
        var report = new Report();
        report.Manufacturing = await _reportManager.ManufacturingReport(searchDateVM);
        return Json(new { report.Manufacturing });
    }
}
