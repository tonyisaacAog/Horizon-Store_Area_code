namespace Horizon.Areas.Sales.ViewModel.reports
{
    public class SearchDateVM
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class SaleReport
    {
        public SaleReport()
        {
            SearchDate = new SearchDateVM();
            SaleVMs = new List<SaleVM>();
        }

        public SearchDateVM SearchDate { get; set; }
        public List<SaleVM>  SaleVMs { get; set; }
    }
}
