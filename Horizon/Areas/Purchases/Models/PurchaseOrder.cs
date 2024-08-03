using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.Models
{
    [Table("Finance_PurchasingModule_PurchaseOrder")]
    public class PurchaseOrder : BaseEntity
    {
        public string? PurchaseOrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateStoreInStock { get; set; }
        public bool IsStoreInStock { get; set; } = false;
        public decimal TotalAmount { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
        public ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }

    }
}
