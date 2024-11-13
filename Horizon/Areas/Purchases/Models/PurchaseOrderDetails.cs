using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Models.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.Models
{
    [Table("Finance_PurchasingModule_PurchaseOrderDetails")]
    public class PurchaseOrderDetails : BaseEntity
    {
        public decimal StoreItemAmount { get; set; }
        public string? Notes { get; set; }
        public bool IsCreatedASPurchasing { get; set; } = false;
        public DetailType DetailType { get; set; }
        public int? StoreItemId { get; set; }
        [ForeignKey(nameof(StoreItemId))]
        public StoreItem? StoreItem { get; set; }

        public int? StoreItemsRawId { get; set; }
        [ForeignKey(nameof(StoreItemsRawId))]
        public StoreItemsRaw? StoreItemsRaw { get; set; }
        public int PurchaseOrderId { get; set; }
        [ForeignKey(nameof(PurchaseOrderId))]
        public PurchaseOrder PurchaseOrder { get; set; }

    }
}
