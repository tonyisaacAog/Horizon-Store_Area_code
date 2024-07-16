using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.Models
{
    [Table("Finance_OrderModule_OrderDetails")]
    public class OrderDetails:BaseEntity
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public StoreItem Product { get; set; }
        public int QTY { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public string? Notes { get; set; }
        public int? ManfactId { get; set; }
        public bool IsManufacturing { get; set; } = false;

        public List<OrderConfigure> OrderConfigure { get; set; } = new List<OrderConfigure>();
    }
}
