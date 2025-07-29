using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Horizon.Services;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreLocationsController : BaseController<StoreLocations, StoreLocationsVM>
    {
        private readonly IMessageService _messageService;

        public StoreLocationsController(GenericSettingsManager<StoreLocations, StoreLocationsVM> StoreLocationsManager, IMessageService messageService) : base(StoreLocationsManager,
            @"/Store/StoreLocations/SaveRecord",
            @"/Store/StoreLocations/Index", messageService)
        {
            _messageService = messageService;
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var storelst = await _settingsManager.GetAll();
            var data = storelst.Select(x => new
            {
                Id = x.Id,
                Name = x.LocationName
            });

            return Json(new { data });
        }

    }
}
