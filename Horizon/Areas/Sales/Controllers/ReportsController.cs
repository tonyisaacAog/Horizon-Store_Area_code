using Horizon.Areas.Sales.Services;
using Horizon.Areas.Sales.ViewModel.reports;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class ReportsController : Controller
    {
        private readonly ReportManager _reportManager;

        public ReportsController(ReportManager reportManager)
        {
            _reportManager = reportManager;
        }
        public IActionResult SalesBetweenDate()
        {
            var reportVM = new SaleReport();
            return View(reportVM);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> LoadReport([FromBody] SearchDateVM searchDateVM)
        {
            var report = new SaleReport();
            report.SaleVMs = await _reportManager.ReprotSaleDateRange(searchDateVM);
            return Json(new { SaleVMs = report.SaleVMs });
        }
    }
}
