using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class ClientController : BaseController<Client, ClientVM>
    {
        public ClientController(GenericSettingsManager<Client, ClientVM> settingsManager) : 
            base(settingsManager,
                @"/Sales/Client/SaveRecord",
                @"/Sales/Client/Index?success")
        {
        }
    }
}
