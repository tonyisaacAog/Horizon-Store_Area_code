using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.ViewModel.Main;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreItemRawDestroyVM
    {
        public StoreItemRawDestroyVM()
        {
            StoreItemRaw = new();
            //Purchasing = new();
        }
        public StoreItemRawVM StoreItemRaw { get; set; }
        //public PurchasingVM Purchasing { get; set; }
        public int PurchasingId { get; set; }
        public decimal DestroyQty { get; set; }
        public string DateOfDestroy { get; set; }
        public string Description { get; set; }
        public string? SaveUrl { get; set; }
    }
}

