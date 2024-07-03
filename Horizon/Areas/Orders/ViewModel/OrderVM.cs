using AutoMapper;
using BaseEntities;
using Horizon.Areas.Orders.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.ViewModel
{
    public class OrderVM: BaseId, IHaveCustomMappings
    {
        //public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? Phone1 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string? OrderDate { get; set; }
        public string DeliveryOrder { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool IsProcess { get; set; } = false;

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<Order, OrderVM>()
                .ForMember(x => x.OrderDate, y => y.MapFrom(obj => obj.OrderDate.ToEgyptianDate()))
                .ForMember(x => x.DeliveryOrder, y => y.MapFrom(obj => obj.DeliveryOrder.ToEgyptianDate()))
                .ForMember(x => x.ClientName, y => y.MapFrom(obj => obj.Client.ClientName))
                .ForMember(x => x.Phone1, y => y.MapFrom(obj => obj.Client.Phone1));
            configuration.CreateMap<OrderVM, Order>()
               .ForMember(x => x.OrderDate, y => y.MapFrom(obj => obj.OrderDate.ToEgyptionDate()))
               .ForMember(x => x.DeliveryOrder, y => y.MapFrom(obj => obj.DeliveryOrder.ToEgyptionDate()));
        }
    }
}
