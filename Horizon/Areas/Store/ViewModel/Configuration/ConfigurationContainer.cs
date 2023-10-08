using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Store.ViewModel.Configuration
{
    public class ConfigurationContainer
    {
        public ConfigurationContainer()
        {
            StoreItemVM = new StoreItemVM();
            ItemConfigurationVMs = new List<ItemConfigurationVM>();
        }
        public int NumberProductCanMade { get; set; } = 0;
        public StoreItemVM StoreItemVM { get; set; }
        public List<ItemConfigurationVM> ItemConfigurationVMs { get; set; }
    }
}
