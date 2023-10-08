using Horizon.Areas.Purchases.ViewModel;

namespace Horizon.Areas.Manufacturing.VewModels.reports
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
            Manufacturing = new List<ManufacturingInfoVM>();
        }

        public SearchDateVM SearchDate { get; set; }
        public List<ManufacturingInfoVM> Manufacturing { get; set; }
    }
}
