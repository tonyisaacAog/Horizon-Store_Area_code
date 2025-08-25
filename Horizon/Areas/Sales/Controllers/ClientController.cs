using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.Services;
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
        private readonly ClientManager _clientManager;
        private readonly IMessageService _messageService;
        public ClientController(ClientManager clientManager, IMessageService messageService) :
            base(clientManager,
                @"/Sales/Client/SaveRecord",
                @"/Sales/Client/Index",
                messageService)
        {
            _messageService = messageService;
            _clientManager = clientManager;
        }



        [HttpGet]
        public async Task<IActionResult> GetClients(string term, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new { items = Array.Empty<object>(), hasMore = false });

            var (items, totalCount) = await _clientManager.SearchClients(term, page, pageSize);

            bool hasMore = totalCount > page * pageSize;

            return Json(new { items, hasMore });
        }
    }
}
