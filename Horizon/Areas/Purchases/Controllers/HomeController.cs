using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class HomeController : Controller
    {
        private readonly PurchaseManager _purchaseManager;
        private readonly PurchaseOrderManager _purchaseOrderManager;

        public HomeController(PurchaseManager purchaseManager,PurchaseOrderManager purchaseOrderManager)
        {
            _purchaseManager = purchaseManager;
            _purchaseOrderManager = purchaseOrderManager;

        }

        public async Task<IActionResult> Index()
        {
            var PurchasesList = await _purchaseManager.GetAll();
            return View(PurchasesList);
        }


        public async Task<IActionResult> DetailsPurchase(int Id)
        {
            var purchaseDetails = await _purchaseManager.DetailsPurchase(Id);
            return View(purchaseDetails);
        }

        public async Task<IActionResult> ManagePurchase( int Id)
        => View(await _purchaseManager.NewPurchase(Id));
        
        public async Task<JsonResult> SavePurchase([FromBody] PurchaseContainer vm)
        {
            var feedback = await _purchaseManager.SavePurchase(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Purchases/Home/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }




        //code for create purchases for specific product 
        public async Task<IActionResult> ManagePurchaseForProduct(int Id)
        {
            var purchaseOrders = await _purchaseOrderManager.GetAllNotStoreInStockContainStoreItem(Id);
            ViewBag.PurchaseOrders = purchaseOrders
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.PurchaseOrderNumber + " " + x.SupplierName
                }).ToArray();
            return View(await _purchaseManager.NewPurchaseForProduct(Id));
        }
        //public async Task<IActionResult> ManagePurchaseRawForProduct(int Id)
        //{
           
        //    return View(await _purchaseManager.NewPurchaseStoreRawForProduct(Id));
        //}
        
        public async Task<JsonResult> SavePurchaseForProduct([FromBody] PurchaseContainerForProduct vm)
        {
            var feedback = await _purchaseManager.SavePurchaseForProduct(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Store/StoreItems/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }


    }
}
