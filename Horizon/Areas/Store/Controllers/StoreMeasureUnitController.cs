using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreMeasureUnitController : BaseController<StoreMeasureUnit,StoreMeasureUnitVM>
    {
        public StoreMeasureUnitController(GenericSettingsManager<StoreMeasureUnit, StoreMeasureUnitVM> StoreMeasureUnitManager) : base(StoreMeasureUnitManager,
            @"/Store/StoreMeasureUnit/SaveRecord",
            @"/Store/StoreMeasureUnit/Index?success")
        {

        }
    }
}
