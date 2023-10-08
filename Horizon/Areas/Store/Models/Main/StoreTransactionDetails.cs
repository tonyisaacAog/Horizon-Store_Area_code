using BaseEntities;
using Finance.CurrentAssetModule.Store.Model.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Stores.Model.Main
{
    [Table("Finance_CurrentAssets_Store_StoreTransactionDetails")]

    public class StoreTransactionDetails:BaseEntity
    {        
        public int StoreTransId { get; set; }
        [ForeignKey("StoreTransId")]
        public StoreTransactions StoreTransactions { get; set; }
        public int StoreItemId { get; set; }
        [ForeignKey("StoreItemId")]
        public StoreItem StoreItem { get; set; }
        public int QTY { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int? QtyBalanceAfterSource { get; set; }
        public int? QtyBalanceAfterDestination { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AmountBalanceAfter { get; set; }
        public int? ManfactId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ManfactUnitCost { get; set; }

    }
}
