using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Sales.Services;
using Horizon.Areas.Sales.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class HomeController : Controller
    {

        private readonly SalesManager _SalesManager;

        public HomeController(SalesManager salesManager)
        {
            _SalesManager = salesManager;
        }

        public async Task<IActionResult> Index()
        {
            var PurchasesList = await _SalesManager.GetAll();
            return View(PurchasesList);
        }

        public async Task<IActionResult> ManageSales(int Id)
            => View(await _SalesManager.NewSales(Id));

        public async Task<IActionResult> ManageSalesForOrder(int Id)
        {
            var vm = await _SalesManager.NewSalesForOrder(Id);
            return View(vm);
        }

        public async Task<IActionResult> DetailsSales(int Id)
        {
            var salesDetails = await _SalesManager.DetailsSales(Id);
            return View(salesDetails);
        }

        public async Task<JsonResult> SaveSales([FromBody] SalesContainer vm)
        {
            var feedback = await _SalesManager.SaveSales(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Sales/Home/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

    }
}
