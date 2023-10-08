using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Reports;
using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ProductConfiguration
    {
        public ProductConfiguration()
        {
            StoreItemVM = new StoreItemVM();
            ItemConfigurationVM = new List<ItemConfigurationVM>();
        }
        public int Id { get; set; }
        public StoreItemVM StoreItemVM { get; set; }
        public List<ItemConfigurationVM> ItemConfigurationVM { get; set; }
    }
}
