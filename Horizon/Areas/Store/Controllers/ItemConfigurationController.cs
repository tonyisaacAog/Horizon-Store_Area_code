using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Configuration;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class ItemConfigurationController : Controller
    {
        private readonly ItemConfigureManager _itemConfigureManager;

        public ItemConfigurationController(ItemConfigureManager ItemConfigureManager)
        {
            _itemConfigureManager = ItemConfigureManager;
        }


        //public async Task<IActionResult> DetailsItemConfiguration(int Id)
        //{
        //    return View(await _itemConfigureManager.ConfigurationDetails(Id));
        //}

        public async Task<IActionResult> ManageConfiguration(int Id)
        => View(await _itemConfigureManager.GetDataStoreItem(Id, x => x.StoreItemId == Id));
        
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveConfiguration([FromBody] ConfigurationContainer vm)
        {
            var feedback = await _itemConfigureManager.SaveConfiguration(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Store/StoreItems/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }
    }
}
