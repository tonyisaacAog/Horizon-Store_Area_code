using BaseEntities;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Store.Models.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.Models
{
    [Table("Finance_OrderModule_OrderConfigure")]
    public class OrderConfigure:BaseEntity
    {
        public int StoreItemId { get; set; }
        [ForeignKey("StoreItemId")]
        public StoreItemsRaw StoreItems { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }


        [StringLength(500)]
        public string? Descrpition { get; set; }

        public bool IsNew { get; set; } = false;
        public int OrderDetailsId { get; set; }
        [ForeignKey(nameof(OrderDetailsId))]
        public OrderDetails OrderDetails { get; set; }

    }
}
