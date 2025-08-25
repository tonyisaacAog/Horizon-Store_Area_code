using Horizon.Areas.Store.ViewModel.Transaction;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class PurchaseContainerForItemRaw
    {
        public PurchaseContainerForItemRaw()
        {
            PurchaseInfo = new();
            PurchaseDetails = new();
        }
        public string? SupplierName { get; set; }
        public int SupplierId { get; set; }
        public int PurchaseOrderId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public PurchaseInfoVM PurchaseInfo { get; set; }
        public List<PurchaseStoreTransactionVM> PurchaseDetails { get; set; }
    }
}
