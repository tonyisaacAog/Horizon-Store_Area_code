using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreLocationsController : BaseController<StoreLocations,StoreLocationsVM>
    {
        public StoreLocationsController(GenericSettingsManager<StoreLocations, StoreLocationsVM> StoreLocationsManager) : base(StoreLocationsManager,
            @"/Store/StoreLocations/SaveRecord",
            @"/Store/StoreLocations/Index?success")
        {

        }

    }
}
