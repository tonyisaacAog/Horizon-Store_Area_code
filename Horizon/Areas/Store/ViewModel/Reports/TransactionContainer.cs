namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class TransactionContainer
    {
        public TransactionContainer()
        {
            Search = new SearchVM();
            TransactionDetails = new List<StoreTransactionDetailsVM>();
        }
        public SearchVM Search { get; set; }
        public List<StoreTransactionDetailsVM> TransactionDetails { get; set; }
    }
}
