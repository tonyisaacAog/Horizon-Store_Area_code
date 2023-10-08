using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ManufacturingContainerDisplay
    {
        public ManufacturingContainerDisplay()
        {
            ProductConfigurations = new ProductConfigurationDisplay();
            ManufacturingInfoVM = new ManufacturingInfoVM();
        }

        [Required]
        public int StoreLocationId { get; set; }
        public string? StoreLocationName { get; set; }
        public ProductConfigurationDisplay ProductConfigurations { get; set; }
        public ManufacturingInfoVM ManufacturingInfoVM { get; set; }
    }
}
