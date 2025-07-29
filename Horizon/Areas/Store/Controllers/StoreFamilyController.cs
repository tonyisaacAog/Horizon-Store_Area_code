using Finance.CurrentAssetModule.Stores.Model.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Horizon.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreFamilyController : BaseController<StoreFamily, StoreFamilyVM>
    {
        private readonly IMessageService _messageService;

        public StoreFamilyController(GenericSettingsManager<StoreFamily, StoreFamilyVM> settingsManager, IMessageService messageService)
            : base(settingsManager,
                  @"/Store/StoreFamily/SaveRecord",
                  @"/Store/StoreFamily/Index", messageService)
        {
            _messageService = messageService;
        }
    }
}
