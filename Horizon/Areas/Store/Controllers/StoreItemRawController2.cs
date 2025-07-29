using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Controllers;
using Horizon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Horizon.Services;


namespace Horizon.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreItemRawController : BaseController<StoreItemsRaw, StoreItemRawVM>
    {

        private readonly ApplicationDbContext _db;
        private readonly IMessageService _messageService;

        public StoreItemRawController(GenericSettingsManager<StoreItemsRaw, StoreItemRawVM> StoreItemRawManager,
            ApplicationDbContext db, IMessageService messageService) : base(StoreItemRawManager,
            @"/Store/StoreItemRaw/SaveRecord",
            @"/Store/StoreItemRaw/Index", messageService)
        {
            _db = db;
            _messageService = messageService;
        }
        public override async Task<IActionResult> Index()
        {
            return View(await _db.StoreItemsRaw.Include(v => v.RawItemType).Where(s => s.RawItemType.Id > 1).ToListAsync());
        }

    }
}
