using AutoMapper;
using BaseEntities;
using Horizon.Areas.Orders.Models;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Orders.ViewModel
{
    public class OrderDetailsVM: BaseId,IHaveCustomMappings
    {
     
        //public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        [Required]
        public int QTY { get; set; }
        public decimal UnitPrice { get; set; }
        public int? ManfactId { get; set; }
        public string? Notes { get; set; }
        public bool IsManufacturing { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<OrderDetails, OrderDetailsVM>()
                .ForMember(x => x.ProductName, y => y.MapFrom(obj => obj.Product.ProductName));
            configuration.CreateMap<OrderDetailsVM, OrderDetails>();
        }
    }
}
