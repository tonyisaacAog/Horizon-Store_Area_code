﻿using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreItemsController : Controller
    {
        private readonly StoreItemManager _settingsManager;
        public StoreItemsController(StoreItemManager settingsManager) 
        {
            _settingsManager = settingsManager;
        }
        public virtual async Task<IActionResult> Index()
        {
            return View(await _settingsManager.GetAll());
        }

        public async Task<IActionResult> ManageDestoryAmount(int Id)
        {
            var storeItemVM = await _settingsManager.CheckAndReturnWithRef(Id,rf=>rf.StoreBrand,r=>r.Family);
            var vm = new StoreItemDestoryVM();
            vm.StoreItem = storeItemVM;
            vm.SaveUrl = @"/Store/StoreItems/SaveDestoryAmount";
            return View(vm);
        }

        public async Task<IActionResult> ManageRecord(int Id)
        {

            if (Id == 0)
            {
                var vm = (StoreItemVM)Activator.CreateInstance(typeof(StoreItemVM));
                vm.SaveUrl = @"/Store/StoreItems/SaveRecord";
                return View(vm);
            }
            else
            {
                var vm = await _settingsManager.CheckAndReturn(Id);
                vm.SaveUrl = @"/Store/StoreItems/SaveRecord";
                return View(vm);
            }

        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveRecord([FromBody] StoreItemVM vm)
        {
            var feedback = await _settingsManager.SaveManagerData(vm);
            if (feedback.Done)
                return Json(new { newLocation = @"/Store/StoreItems/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveDestoryAmount([FromBody] StoreItemDestoryVM vm)
        {
            var feedback = await _settingsManager.SaveDestroyOperation(vm);
            if (feedback.Done)
                return Json(new { newLocation = @"/Store/StoreItems/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }
    }
}
