using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Purchases.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Sales.Models
{
    [Table("Finance_SalesModule_Sale")]
    public class Sale : BaseEntity
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? SalesDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }
        public IEnumerable<SaleDetails> SaleDetails { get; set; } 

    }
}
