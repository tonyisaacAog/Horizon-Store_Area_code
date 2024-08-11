using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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

        public int StoreItemId { get; set; } = 0;
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; } 
    }
}
