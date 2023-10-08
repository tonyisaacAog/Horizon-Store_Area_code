using BaseEntities;
using Horizon.Areas.Sales.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.Models
{
    [Table("Finance_OrderModule_Order")]
    public class Order: BaseEntity
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? OrderDate { get; set; }
        public bool IsProcess { get; set; } = false;
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
