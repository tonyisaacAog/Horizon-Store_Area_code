using Horizon.Areas.Store.ViewModel.ItemRawReport;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class AmountNotCollectReportContainer
    {
        public AmountNotCollectReportContainer()
        {
            Items = new();
            Search = new();
        }
        public SearchForProductVM Search { get; set; } 
        public List<StoreItemAmountNotCollectReportVM> Items { get; set; }
    }
  
}
