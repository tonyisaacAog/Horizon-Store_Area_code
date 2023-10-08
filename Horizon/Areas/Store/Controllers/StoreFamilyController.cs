using Finance.CurrentAssetModule.Stores.Model.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreFamilyController : BaseController<StoreFamily, StoreFamilyVM>
    {
        public StoreFamilyController(GenericSettingsManager<StoreFamily, StoreFamilyVM> settingsManager) 
            : base(settingsManager,
                  @"/Store/StoreFamily/SaveRecord",
                  @"/Store/StoreFamily/Index?success")
        {
        }
    }
}
