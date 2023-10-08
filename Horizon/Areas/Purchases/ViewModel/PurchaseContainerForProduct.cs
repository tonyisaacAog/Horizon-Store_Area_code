using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Transaction;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class PurchaseContainerForProduct
    {
        public PurchaseContainerForProduct()
        {
            PurchaseInfo = new();
            PurchaseDetails = new();
            StoreItem = new();
        }
        public StoreItemVM StoreItem { get; set; }
        public int SupplierId { get; set; }
        public PurchaseInfoVM PurchaseInfo { get; set; }
        public List<PurchaseStoreTransactionVM> PurchaseDetails { get; set; }
    }
}
