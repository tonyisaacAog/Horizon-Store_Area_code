using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Services;
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
        private readonly IMessageService _messageService;

        public HomeController(PurchaseManager purchaseManager, PurchaseOrderManager purchaseOrderManager, IMessageService messageService)
        {
            _purchaseManager = purchaseManager;
            _purchaseOrderManager = purchaseOrderManager;
            _messageService = messageService;
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
        public async Task<IActionResult> GetDetailsPurchase(int Id)
        {
            var purchaseDetails = await _purchaseManager.GetDetailsPurchase(Id);
            return View(purchaseDetails);
        }

        public async Task<IActionResult> ManagePurchase(int Id)
        => View(await _purchaseManager.NewPurchase(Id));

        public async Task<JsonResult> SavePurchase([FromBody] PurchaseContainer vm)
        {
            var feedback = await _purchaseManager.SavePurchase(vm);
            if (feedback.Done)
            {
                _messageService.Success("تم حفظ البيانات بنجاح");
                return Json(new { newLocation = "/Purchases/Home/Index" });
            }
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

        public async Task<IActionResult> ManagePurchaseForItemRaw(int Id)
        {
            var purchaseOrders = await _purchaseOrderManager.GetAllNotStoreInStockContainStoreItemRaw(Id);
            if(purchaseOrders == null)
            {
                _messageService.Error("لا يمكن عمل اذن من امر الانتاج هذا");
            }
            return View(purchaseOrders);
        }
        //public async Task<IActionResult> ManagePurchaseRawForProduct(int Id)
        //{

        //    return View(await _purchaseManager.NewPurchaseStoreRawForProduct(Id));
        //}

        public async Task<JsonResult> SavePurchaseForProduct([FromBody] PurchaseContainerForProduct vm)
        {
            var feedback = await _purchaseManager.SavePurchaseForProduct(vm);
            if (feedback.Done)
            {
                _messageService.Success("تم انشاء اذن اضافة خامات");
                return Json(new { newLocation = "/Store/StoreItems/Index" });
            }
            else
            {
                _messageService.Success("فشل انشاء اذن اضافة خامات");
                return Json(new { errors = feedback.Messages });
            }
        }

        public async Task<JsonResult> SavePurchaseForItemRaw([FromBody] PurchaseContainerForItemRaw vm)
        {
            var feedback = await _purchaseManager.SavePurchaseForItemRaw(vm);
            if (feedback.Done)
            {
                _messageService.Success("تم انشاء اذن اضافة خامات");
                return Json(new { newLocation = "/Store/StoreItems/Index" });
            }
            else
            {
                _messageService.Success("فشل انشاء اذن اضافة خامات");
                return Json(new { errors = feedback.Messages });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchaseBySearchValue(string term, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new { items = Array.Empty<object>(), hasMore = false });

            var (items, totalCount) = await _purchaseManager.GetPurchaseBySearchValue(term, page, pageSize);

            bool hasMore = totalCount > page * pageSize;

            return Json(new { items, hasMore });
        }



    }
}
