namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class SearchVM
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StoreItemId { get; set; }
        public int? StartBalance { get; set; }
        public int? EndBalance { get; set; } 
    }
}
