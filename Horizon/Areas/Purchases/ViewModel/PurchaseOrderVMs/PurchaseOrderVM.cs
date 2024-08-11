using AutoMapper;
using BaseEntities;
using Horizon.Areas.Purchases.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs
{
    public class PurchaseOrderVM : BaseId, IHaveCustomMappings
    {
        public PurchaseOrderVM() { PurchaseOrderDetails = new List<PurchaseOrderDetailsVM>(); }
        public string? PurchaseOrderNumber { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string? DateStoreInStock { get; set; }
        public bool IsStoreInStock { get; set; } = false;
        public decimal TotalAmount { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public List<PurchaseOrderDetailsVM> PurchaseOrderDetails { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<PurchaseOrderVM, PurchaseOrder>()
                .ForMember(x => x.Date, y => y.MapFrom(obj => obj.PurchaseOrderDate.ToEgyptionDate()));
            configuration.CreateMap<PurchaseOrder, PurchaseOrderVM>()
                .ForMember(x => x.PurchaseOrderDate, y => y.MapFrom(obj => obj.Date.ToEgyptianDate()))
                .ForMember(x => x.SupplierName, y => y.MapFrom(obj => obj.Supplier.SupplierName??"N/A"));

        }
    }
}
