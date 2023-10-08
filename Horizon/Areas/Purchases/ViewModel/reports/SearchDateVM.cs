namespace Horizon.Areas.Purchases.ViewModel.reports
{
    public class SearchDateVM
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class Report
    {
        public Report()
        {
            SearchDate = new SearchDateVM();
            Purchasings = new List<PurchasingVM>();
        }

        public SearchDateVM SearchDate { get; set; }
        public List<PurchasingVM> Purchasings { get; set; }
    }
}
