
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel.reports;
using JasperFx.CodeGeneration.Frames;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class ReportsController : Controller
    {
        private readonly ReportManager _reportManager;

        public ReportsController(ReportManager reportManager)
        {
            _reportManager = reportManager;
        }
        public IActionResult PurchaesBetweenDate()
        {
            var reportVM = new Report();
            return View(reportVM);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> LoadReport([FromBody] SearchDateVM searchDateVM)
        {
            var report = new Report();
            report.Purchasings = await _reportManager.ReprotPurchasingDateRange(searchDateVM);
            return Json(new { Purchasings = report.Purchasings });
        }
    }
}
