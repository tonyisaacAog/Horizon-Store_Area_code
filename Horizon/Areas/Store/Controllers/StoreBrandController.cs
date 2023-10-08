using Finance.CurrentAssetModule.Stores.Model.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreBrandController : BaseController<StoreBrand,StoreBrandVM>
    {
        public StoreBrandController
            (GenericSettingsManager<StoreBrand, StoreBrandVM> settingsManager): base(settingsManager,
                  @"/Store/StoreBrand/SaveRecord",
                  @"/Store/StoreBrand/Index?success")
        {
        }

        
    }
}
