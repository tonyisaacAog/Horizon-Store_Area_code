using Microsoft.Build.Framework;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ManufacturingContainer
    {
        public ManufacturingContainer()
        {
            ProductConfigurations =new ProductConfiguration();
            ManufacturingInfoVM = new ManufacturingInfoVM();
        }

        [Required]
        public int StoreLocationId { get; set; }
        public string? StoreLocationName { get; set; }
        public string? RedirectUrl { get; set; }
        public int param { get; set; }
        public ProductConfiguration ProductConfigurations { get; set; }
        public ManufacturingInfoVM ManufacturingInfoVM { get; set; }
    }

}
