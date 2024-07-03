using Horizon.Areas.Manufacturing.Services;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Configuration;
using Horizon.Areas.Store.ViewModel.Settings;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Manufacturing.Controllers
{
    [Area("Manufacturing")]
    public class HomeController : Controller
    {
        private readonly ManufacturingManager _ManufacturingManager;
        private readonly GenericSettingsManager<StoreItemsRaw, StoreItemRawVM>
            _StoreItemRawManager;

        public HomeController(
            ManufacturingManager ManufacturingManager,
            GenericSettingsManager<StoreItemsRaw,StoreItemRawVM> StoreItemRawManager)
        {
            _ManufacturingManager = ManufacturingManager;
            _StoreItemRawManager = StoreItemRawManager;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var vm = await _ManufacturingManager.GetConfigurationProduct(Id);
            return View(vm);
        }


        public async Task<IActionResult> ManageManufactingForOrder(int Id)
        {
            var container =await _ManufacturingManager.GetExtraConfigurationForOrder(Id);
            return View(container);
        }

        public async Task<IActionResult> DetailsManufacturing(int Id)
        {
            var salesDetails = await _ManufacturingManager.DetailsManufacturing(Id);
            return View(salesDetails);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveManufacturing([FromBody] ManufacturingContainer vm)
        {
            var feedback = await _ManufacturingManager.SaveManufacturing(vm);
            if (feedback.Done)
                return Json(new { newLocation = vm.RedirectUrl!=null?vm.RedirectUrl: "/Store/StoreItems/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

        public async Task<JsonResult> GetDataItemRaw(int Id)
        {
            if(Id<1) return Json(new { errors = "اختر المادة الخام التى تريدها" });
            var vm = await _StoreItemRawManager.CheckAndReturn(Id);
            return Json( vm );
        }
    }
}
