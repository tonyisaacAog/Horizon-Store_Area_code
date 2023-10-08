namespace Horizon.Areas.Store.ViewModel.ItemRawReport
{
    public class SearchVM
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StoreItemId { get; set; }
        public decimal? StartBalance { get; set; } = 0;
        public decimal? EndBalance { get; set; } = 0;
    }
}
