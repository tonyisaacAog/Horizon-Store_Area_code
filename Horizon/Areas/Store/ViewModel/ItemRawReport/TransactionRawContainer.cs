namespace Horizon.Areas.Store.ViewModel.ItemRawReport
{
    public class TransactionRawContainer
    {
        public TransactionRawContainer()
        {
            Search = new SearchVM();
            StoreItemRawTransactions = new List<StoreItemRawTransactionVM>();
        }
        public SearchVM Search { get; set; }
        public List<StoreItemRawTransactionVM> StoreItemRawTransactions { get; set; }
    }
}
