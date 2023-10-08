
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
        }
        public ClientVM Client { get; set; }
        public SaleInfoVM SaleInfo { get; set; }
        public List<SaleStoreTransactionVM> SaleDetails { get; set; }
    }
}
