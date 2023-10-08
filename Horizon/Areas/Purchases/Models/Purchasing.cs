using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BaseEntities;
using MyInfrastructure.Extensions;

namespace Horizon.Areas.Purchases.Models
{
    [Table("Finance_PurchasingModule_Purchasing")]
    public class Purchasing:BaseEntity
    {
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
      
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? PurchasingDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }

        public override string ToString()
        {
            return $"اسم العميل :{Supplier?.SupplierName} | تاريخ الفاتورة : {PurchasingDate.ToEgyptianDate()} | رقم الفاتورة : {InvoiceNum}";
        }

    }
}
