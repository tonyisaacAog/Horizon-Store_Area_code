
using Horizon.Areas.Store.ViewModel.Transaction;

namespace Horizon.Areas.Sales.ViewModel
{
    public class SalesContainer
    {
        public SalesContainer()
        {
            Client = new();
            SaleInfo = new();
            SaleDetails = new();
            SaleItemRawDetails = new();
        }
        public ClientVM Client { get; set; }
        public SaleInfoVM SaleInfo { get; set; }
        public List<SaleStoreTransactionVM> SaleDetails { get; set; }
        public List<SaleStoreTransactionVM> SaleItemRawDetails { get; set; }
        public bool IsSaleFromOrder { get; set; }
        public int OrderId { get; set; }
    }
}
