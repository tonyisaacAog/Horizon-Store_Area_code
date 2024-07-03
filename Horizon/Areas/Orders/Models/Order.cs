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
        public string? NoOfOrder { get; private set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DeliveryOrder { get; set; }
        public bool IsProcess { get; set; } = false;
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        
        public static void GenerateSerial(Order order)
        {
            order.NoOfOrder = $"{order.Id}/{DateTime.Now.Year}";
        }
    }
}
