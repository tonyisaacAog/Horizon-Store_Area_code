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

        public async Task<IActionResult> GetAmountBalanceStoreItemRaw(int id,string WarningLimit)
        {
            var storeItemRawBalance = await _reportManager.GetAmountBalanceStoreItemRaw(id,WarningLimit);
            ViewBag.WarningLimit = WarningLimit?.ToLower().Trim() == "on" ? true : false;
            ViewBag.Id = id;
            return View(storeItemRawBalance);
        }
        //public async Task<IActionResult> GetAmountBalanceStoreItemRawWithType(string type)
        //{

        //    var storeItemRawBalance = await _reportManager.GetAmountBalanceStoreItemRaw(type);
        //    return View(storeItemRawBalance);
        //}
        public async Task<IActionResult> GetAmountBalanceStoreItemInStore()
        {
            var storeItemBalance = await _reportManager.GetAmountBalanceStoreItemInStore();
            return View(storeItemBalance);
        }
        public async Task<IActionResult> GetAmountBalanceStoreItem()
        {
            var storeItemBalance = await _reportManager.GetAmountBalanceStoreItem();
            return View(storeItemBalance);
        }


        public async Task<IActionResult> GetAmountBalanceStoreItemNotCollect()
        {
            var storeItemBalance = new AmountNotCollectReportContainer();
            return View(storeItemBalance);
        }
        [HttpGet]
        public async Task<IActionResult> GetAmountBalanceStoreItemNotCollectFromPurchase()
        {
            var result = new AmountNotCollectReportContainer();
            ViewBag.Errors = string.Empty;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAmountBalanceStoreItemNotCollectFromPurchase([FromForm] SearchForProductVM search)
        {
            
            var result = new AmountNotCollectReportContainer();
            if(string.IsNullOrEmpty(search.StartDate)|| string.IsNullOrEmpty(search.EndDate) )
            {
                ViewBag.Errors ="اختر التاريخ";
                return View(result);
            }
            var storeItemBalance = await _reportManager.GetAmountBalanceStoreItemNotCollectFromPurchase(search);
            result.Search = search;
            result.Items = storeItemBalance;
            return View(result);
        }
        public async Task<IActionResult> GetAmountBalanceStoreItemNotCollectPurchQty()
        {
            var result = new AmountNotCollectReportContainer();
            ViewBag.Errors = string.Empty;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAmountBalanceStoreItemNotCollectPurchQty([FromForm] SearchForProductVM search)
        {
            var result = new AmountNotCollectReportContainer();
            if( string.IsNullOrEmpty(search.StartDate) || string.IsNullOrEmpty(search.EndDate) )
            {
                ViewBag.Errors = "اختر التاريخ";
                return View(result);
            }
            var storeItemBalance = await _reportManager.GetAmountBalanceStoreItemNotCollectFromPurchaseQty(search);
            result.Search = search;
            result.Items = storeItemBalance;
            return View(result);
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

        public async Task<IActionResult> ItemRawForProduct()
        {
            return View(new TransactionRawContainerForProduct());
        }
        [HttpPost]
        public async Task<IActionResult> ItemRawForProduct([FromForm]SearchForProductVM search)
        {
            var result = await _reportManager.GetTransactionItemRawForManufactProduct(search);
            
            return View(result);
        }
    }
}
