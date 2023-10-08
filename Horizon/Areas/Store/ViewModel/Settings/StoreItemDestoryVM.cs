using Horizon.Areas.Store.ViewModel.Main;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreItemDestoryVM
    {
        public StoreItemDestoryVM()
        {
            StoreItem = new();
        }
        public StoreItemVM StoreItem { get; set; }
        public string DateOfDestroy { get; set; }
        public int StoreLocationId { get; set; }
        public string Description { get; set; }
        public string? SaveUrl { get; set; }
    }
}
