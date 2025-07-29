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
    public class StoreMeasureUnitController : BaseController<StoreMeasureUnit, StoreMeasureUnitVM>
    {
        private readonly IMessageService _messageService;

        public StoreMeasureUnitController(GenericSettingsManager<StoreMeasureUnit, StoreMeasureUnitVM> StoreMeasureUnitManager, IMessageService messageService) : base(StoreMeasureUnitManager,
            @"/Store/StoreMeasureUnit/SaveRecord",
            @"/Store/StoreMeasureUnit/Index", messageService)
        {
            _messageService = messageService;
        }
    }
}
