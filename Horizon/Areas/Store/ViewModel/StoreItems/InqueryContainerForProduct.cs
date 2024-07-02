using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Transaction;

namespace Horizon.Areas.Store.ViewModel.StoreItems
{
    public class InqueryContainerForProduct
    {
        public InqueryContainerForProduct()
        {
            InqueryDetails = new();
            StoreItem = new();
        }
        public StoreItemVM StoreItem { get; set; }
        public List<InqueryDetailsForProduct> InqueryDetails { get; set; }
    }
}
