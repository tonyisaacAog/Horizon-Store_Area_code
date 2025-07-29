using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
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
    public class RawItemTypeController : BaseController<RawItemType, RawItemTypeVM>
    {
        private readonly IMessageService _messageService;
        public RawItemTypeController(GenericSettingsManager<RawItemType, RawItemTypeVM> RawItemTypeManager, IMessageService messageService) : base(RawItemTypeManager,
            @"/Store/RawItemType/SaveRecord",
            @"/Store/RawItemType/Index", messageService)
        {
            _messageService = messageService;
        }

        public override async Task<IActionResult> ManageRecord(int Id)
        {
            if (Id == 1)
                return Redirect("/Store/RawItemType/Index");
            return await base.ManageRecord(Id);
        }

    }
}
