using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Store.ViewModel.ItemRawReport
{
    public class ItemRawContainer
    {
        public ItemRawContainer()
        {
            StoreItemRaw = new StoreItemRawVM();
            TransactionRawContainer = new TransactionRawContainer();
        }
        public TransactionRawContainer TransactionRawContainer { get; set; }
        public StoreItemRawVM StoreItemRaw { get; set; }
    }
}
