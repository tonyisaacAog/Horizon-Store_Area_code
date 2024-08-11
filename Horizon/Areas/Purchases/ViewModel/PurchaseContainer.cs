using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
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
            PurchaseOrder = new();
        }
        public SupplierVM Supplier { get; set; }
        public PurchaseOrderVM? PurchaseOrder { get; set; }
        public PurchaseInfoVM PurchaseInfo { get; set; }
        public List<PurchaseStoreTransactionVM> PurchaseDetails { get; set; }
    }
}
