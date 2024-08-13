using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Orders.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.Models
{
    [Table("Finance_PurchasingModule_PurchaseOrder")]
    public class PurchaseOrder : BaseEntity
    {
        public string? PurchaseOrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Notes { get; set; }
        public DateTime? DateStoreInStock { get; set; }
        public bool IsStoreInStock { get; set; } = false;
        public decimal TotalAmount { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
        public ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public static void GenerateSerial(PurchaseOrder order)
        {
            order.PurchaseOrderNumber = $"{order.Id}/{DateTime.Now.Year}";
        }

    }
}
