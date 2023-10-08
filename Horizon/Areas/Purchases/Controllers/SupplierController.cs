using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class SupplierController : BaseController<Supplier, SupplierVM>
        
    {
        public SupplierController(GenericSettingsManager<Supplier,SupplierVM> SupplierManager):base(SupplierManager,
            @"/Purchases/Supplier/SaveRecord",
            @"/Purchases/Supplier/Index?success")
        {
            
        }
        
    }
}
