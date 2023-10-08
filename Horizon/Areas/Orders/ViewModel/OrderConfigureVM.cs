using AutoMapper;
using BaseEntities;
using Horizon.Areas.Orders.Models;
using Horizon.Models.Shared;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Orders.ViewModel
{
    public class OrderConfigureVM : BaseId, IHaveCustomMappings
    {
        public int OrderDetailsId { get; set; }
        public int StoreItemId { get; set; }
        public string? StoreItemRaw { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }

        [StringLength(500)]
        public string? Descrpition { get; set; }
        public RecordStatus RecordStatus { get; set; } = RecordStatus.UnChanged;
        public bool IsNew { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<OrderConfigure, OrderConfigureVM>()
                .ForMember(x => x.StoreItemRaw, y => y.MapFrom(obj => obj.StoreItems.ItemName));
            configuration.CreateMap<OrderConfigureVM, OrderConfigure>();

        }
    }
}
