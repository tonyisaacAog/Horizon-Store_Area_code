using Horizon.Areas.Orders.Models;
using Horizon.Areas.Orders.ViewModel;
using Microsoft.Build.Framework;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ManufacturingContainer
    {
        public ManufacturingContainer()
        {
            ProductConfigurations = new ProductConfiguration();
            ManufacturingInfoVM = new ManufacturingInfoVM();
            Order = new();
        }

        [Required]
        public int StoreLocationId { get; set; }
        public string? StoreLocationName { get; set; }
        public string? RedirectUrl { get; set; }
        public int param { get; set; }
        public bool IsManufacturingForOrder { get; set; }
        public OrderVM? Order { get; set; }
        public ProductConfiguration ProductConfigurations { get; set; }
        public ManufacturingInfoVM ManufacturingInfoVM { get; set; }
    }

}
