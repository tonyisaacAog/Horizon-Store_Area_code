namespace Horizon.Areas.Store.ViewModel.StoreItems
{
    public class InqueryDetailsForProduct
    {
        public int StoreItemRawId { get; set; }
        public string StoreItemRawName { get; set; }
        public decimal NumberStoreItemRaw { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal AvailableAmount { get; set; }
        public decimal NeededAmount { get; set; }
    }
}
