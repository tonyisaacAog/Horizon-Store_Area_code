using Finance.CurrentAssetModule.Store.Model.Raw;

namespace Horizon.Areas.Store.ViewModel.ItemRawReport
{
    public class StoreItemRawTransactionVM
    {
        public int Id { get; set; }
        public int StoreItemId { get; set; }
        public string StoreItemRawName { get; set; }
        public decimal QTY { get; set; }
        public StoreRawTransTypeEnum TransType { get; set; }
        public string TransTypeName { get; set; }
        public decimal QtyAfter { get; set; }
        public decimal AmountBalanceAfter { get; set; }
        public string TransDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Descrpition { get; set; }
        //For Purchasing
        public int? ReferanceId { get; set; }
       
    }
}
