using Horizon.Areas.Store.ViewModel.Transaction;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class PurchaseContainer
    {
        public PurchaseContainer()
        {
            Supplier = new();
            PurchaseInfo = new();
            PurchaseDetails = new();
        }
        public SupplierVM Supplier { get; set; }
        public PurchaseInfoVM PurchaseInfo { get; set; }
        public List<PurchaseStoreTransactionVM> PurchaseDetails { get; set; }
    }
}
