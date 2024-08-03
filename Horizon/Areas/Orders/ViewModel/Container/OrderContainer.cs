using AutoMapper;
using BaseEntities;
using Horizon.Areas.Orders.Models;
using Horizon.Areas.Sales.ViewModel;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.ViewModel.Container
{
    public class OrderContainer
    {
        public OrderContainer()
        {
            Order = new();
            Client = new();
            OrderDetail = new();
        }

        public OrderVM Order { get; set; }
        public ClientVM Client { get; set; }
        public List<OrderDetailsVM> OrderDetail { get; set; }
    }
    public class OrderForPersonContainer
    {
        public OrderForPersonContainer()
        {
            Order = new();
            OrderDetail = new();
        }

        public OrderForPersonVM Order { get; set; }
        public List<OrderDetailsVM> OrderDetail { get; set; }
    }
    public class OrderForPersonVM : BaseId, IHaveCustomMappings
    {
        public int ClientId { get; set; } = 1;
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public string? NoOfOrder { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public string? OrderDate { get; set; } = string.Empty;
        public string DeliveryOrder { get; set; } = string.Empty;
        public OrderStatus OrderStatus { get; set; }
        public bool IsProcess { get; set; } = false;

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<Order, OrderForPersonVM>()
                .ForMember(x => x.OrderDate, y => y.MapFrom(obj => obj.OrderDate.ToEgyptianDate()))
                .ForMember(x => x.DeliveryOrder, y => y.MapFrom(obj => obj.DeliveryOrder.ToEgyptianDate()));
            configuration.CreateMap<OrderForPersonVM, Order>()
               .ForMember(x => x.OrderDate, y => y.MapFrom(obj => obj.OrderDate.ToEgyptionDate()))
               .ForMember(x => x.DeliveryOrder, y => y.MapFrom(obj => obj.DeliveryOrder.ToEgyptionDate()));
        }
    }

}
