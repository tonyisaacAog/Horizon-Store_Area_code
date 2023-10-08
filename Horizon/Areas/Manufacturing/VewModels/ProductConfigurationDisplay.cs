using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Reports;
using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Manufacturing.VewModels
{
    public class ProductConfigurationDisplay
    {
        public ProductConfigurationDisplay()
        {
            StoreItemVM = new StoreItemVM();
            ItemConfigurationVM = new List<StoreItemRawTransactionVM>();
        }
        public int Id { get; set; }
        public StoreItemVM StoreItemVM { get; set; }
        public List<StoreItemRawTransactionVM> ItemConfigurationVM { get; set; }
    }
}
