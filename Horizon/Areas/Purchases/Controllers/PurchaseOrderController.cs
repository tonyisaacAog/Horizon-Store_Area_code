using Horizon.Areas.Orders.Models;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Orders.ViewModel.Container;
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class PurchaseOrderController : Controller
    {
       public readonly PurchaseOrderManager _purchaseOrderManager;
        public PurchaseOrderController(PurchaseOrderManager purchaseOrderManager)
        {
            _purchaseOrderManager = purchaseOrderManager;
        }
        public async Task<IActionResult> Index()
        {
            var purchaseOrder = await _purchaseOrderManager.GetAll();
            return View(purchaseOrder);
        }
       
        public async Task<IActionResult> ManagePurchaseOrder(int id)
        {
            if(id > 0){
                var details = await _purchaseOrderManager.PurchaseOrderDetails(id);
                if(details == null)
                {
                    return Redirect("/Purchases/PurchaseOrder/Index");
                }
                if(details.IsStoreInStock ==true|| details.PurchaseOrderDetails.Any(obj=>obj.IsCreatedASPurchasing ==true))
                { return View("Details",details); }
                return View(details);
            }
            return View(new PurchaseOrderVM());
        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SavePurchaseOrder([FromBody] PurchaseOrderVM vm)
        {
            var feedback = await _purchaseOrderManager.SavePurchaseOrder(vm);
            if (feedback.Done)
                return Json(new { newLocation = $"/Purchases/PurchaseOrder/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

    }
}
