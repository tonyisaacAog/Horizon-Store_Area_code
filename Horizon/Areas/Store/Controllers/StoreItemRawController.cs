using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Settings;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreItemRawController : Controller
    {
        protected readonly StoreItemRawManager _settingsManager;

        public StoreItemRawController(StoreItemRawManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public virtual async Task<IActionResult> Index()
        {
            return View(await _settingsManager.GetStoreItemRawNoTeaks());
        }

        public virtual async Task<IActionResult> TeakIndex()
        {
            return View( await _settingsManager.GetStoreItemRawTeaks());
        }

        public async Task<IActionResult> ManageTeakRecord(int Id)
        {

            if (Id == 0)
            {
                var vm = (StoreItemRawVM)Activator.CreateInstance(typeof(StoreItemRawVM));
                vm.RawItemTypeId = 1;
                vm.SaveUrl = @"/Store/StoreItemRaw/SaveRecord";
                vm.RedirectUrl = @"/Store/StoreItemRaw/TeakIndex?success";
                return View(vm);
            }
            else
            {
                var vm = await _settingsManager.CheckAndReturn(Id);
                vm.RawItemTypeId = 1;
                vm.SaveUrl = @"/Store/StoreItemRaw/SaveRecord";
                vm.RedirectUrl = @"/Store/StoreItemRaw/TeakIndex?success";
                return View(vm);
            }
        }

        public async Task<IActionResult> ManageRecord(int Id)
        {

            if (Id == 0)
            {
                var vm = (StoreItemRawVM)Activator.CreateInstance(typeof(StoreItemRawVM));
                vm.SaveUrl = @"/Store/StoreItemRaw/SaveRecord";
                vm.RedirectUrl =  @"/Store/StoreItemRaw/Index?success";
                return View(vm);
            }
            else
            {
                var vm = await _settingsManager.CheckAndReturn(Id);
                vm.SaveUrl = @"/Store/StoreItemRaw/SaveRecord";
                vm.RedirectUrl = @"/Store/StoreItemRaw/Index?success";
                return View(vm);
            }
        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveRecord([FromBody] StoreItemRawVM vm)
        {
            var feedback = await _settingsManager.SaveManagerData(vm);
            if (feedback.Done)
                return Json(new { newLocation = vm.RedirectUrl });
            else
                return Json(new { errors = feedback.Messages });
        }



        public async Task<IActionResult> ManageDestoryAmount(int Id)
        {
            var storeItemRawVM = await _settingsManager.CheckAndReturnWithRef(Id, rf => rf.RawItemType);
            var vm = new StoreItemRawDestroyVM();
            vm.StoreItemRaw = storeItemRawVM;
            vm.SaveUrl = @"/Store/StoreItemRaw/SaveDestoryAmount";
            return View(vm);
        }


        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveDestoryAmount([FromBody] StoreItemRawDestroyVM vm)
        {
            var feedback = await _settingsManager.SaveDestroyOperation(vm);
            if (feedback.Done)
                return Json(new { newLocation = @"/Store/StoreItemRaw/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }
    }
}
