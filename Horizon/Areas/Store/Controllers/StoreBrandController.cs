using Finance.CurrentAssetModule.Stores.Model.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Horizon.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreBrandController : BaseController<StoreBrand, StoreBrandVM>
    {
        private readonly IMessageService _messageService;

        public StoreBrandController
            (GenericSettingsManager<StoreBrand, StoreBrandVM> settingsManager, IMessageService messageService) : base(settingsManager,
                  @"/Store/StoreBrand/SaveRecord",
                  @"/Store/StoreBrand/Index", messageService)
        {
            _messageService = messageService;
        }


    }
}
