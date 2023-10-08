using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class RawItemTypeController : BaseController<RawItemType, RawItemTypeVM>
    {
        public RawItemTypeController(GenericSettingsManager<RawItemType, RawItemTypeVM> RawItemTypeManager) : base(RawItemTypeManager,
            @"/Store/RawItemType/SaveRecord",
            @"/Store/RawItemType/Index?success")
        {

        }

        public override async Task<IActionResult> ManageRecord(int Id)
        {
            if(Id == 1)
                return Redirect("/Store/RawItemType/Index");
            return await base.ManageRecord(Id);
        }
    }
}
