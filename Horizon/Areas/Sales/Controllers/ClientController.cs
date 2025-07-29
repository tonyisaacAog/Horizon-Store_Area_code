using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Controllers;
using Horizon.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class ClientController : BaseController<Client, ClientVM>
    {
        private readonly IMessageService _messageService;

        public ClientController(GenericSettingsManager<Client, ClientVM> settingsManager, IMessageService messageService) :
            base(settingsManager,
                @"/Sales/Client/SaveRecord",
                @"/Sales/Client/Index",
                messageService)
        {
            _messageService = messageService;
        }
    }
}
