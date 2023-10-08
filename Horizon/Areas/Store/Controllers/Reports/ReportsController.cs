using Horizon.Areas.Store.Services.Reports;
using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Horizon.Areas.Store.ViewModel.Reports;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Store.Controllers.Reports
{
    [Area("Store")]
    public class ReportsController : Controller
    {
        private readonly ReportsManager _reportManager;
        public ReportsController(ReportsManager reportManager)
        {
            _reportManager = reportManager;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var card = await _reportManager.GetDataProduct(Id);
            return View(card);
        }


        public async Task<IActionResult> ItemRawIndex(int Id)
        {
            var card = await _reportManager.GetDataStoreItemRaw(Id);
            return View(card);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> LoadReport([FromBody] TransactionContainer searchModel)
        {
            var Card = await _reportManager.GetTransactionProduct(searchModel);
            return Json(new { Result = Card });
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> LoadReportItemRaw([FromBody] TransactionRawContainer  searchModel)
        {
            var Card = await _reportManager.GetTransactionItemRaw(searchModel);
            return Json(new { Result = Card });
        }
    }
}
