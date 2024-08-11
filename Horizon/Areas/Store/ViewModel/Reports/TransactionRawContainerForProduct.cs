using Horizon.Areas.Store.ViewModel.ItemRawReport;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class TransactionRawContainerForProduct
    {
        public TransactionRawContainerForProduct()
        {
            Search = new SearchForProductVM();
            StoreItemRawTransactions = new();
        }
        public SearchForProductVM Search { get; set; }
        public List<TransactionRawContainer> StoreItemRawTransactions { get; set; }
    }
    public class SearchForProductVM
    {
        public int StoreItemId { get; set; } 
        public string StartDate { get; set; } 
        public string EndDate { get; set; } 
    }
}
