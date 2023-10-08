using Horizon.Areas.Store.ViewModel.Main;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class ItemCardContainer
    {
        public ItemCardContainer()
        {
            StoreItem = new StoreItemVM();
            StoreLocationBalances = new List<StoreLocationBalanceVM>();
            TransactionContainer = new TransactionContainer();
        }
        public StoreItemVM StoreItem { get; set; }
        public List<StoreLocationBalanceVM> StoreLocationBalances { get; set; }
        public TransactionContainer TransactionContainer { get; set; }
    }
}
