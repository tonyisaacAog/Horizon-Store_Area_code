using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class SupplierController : BaseController<Supplier, SupplierVM>
        
    {
        private readonly SupplierManager _supplierManager;
        public SupplierController(SupplierManager SupplierManager):base(SupplierManager,
            @"/Purchases/Supplier/SaveRecord",
            @"/Purchases/Supplier/Index?success")
        {
            _supplierManager= SupplierManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers(string term, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new { items = Array.Empty<object>(), hasMore = false });

            var (items, totalCount) = await _supplierManager.SearchSuppliers(term, page, pageSize);

            bool hasMore = totalCount > page * pageSize;

            return Json(new { items, hasMore });
        }
    }
}
