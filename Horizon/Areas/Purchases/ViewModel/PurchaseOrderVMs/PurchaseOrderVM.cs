using AutoMapper;
using BaseEntities;
using Horizon.Areas.Purchases.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs
{
    public class PurchaseOrderVM : BaseId, IHaveCustomMappings
    {
        public PurchaseOrderVM() { PurchaseOrderDetails = new List<PurchaseOrderDetailsVM>(); Notes = new(); }
        public string? PurchaseOrderNumber { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string? DateStoreInStock { get; set; }
        public List<PurchaseOrderNotes>? Notes { get; set; } 
        public bool IsStoreInStock { get; set; } = false;
        public decimal TotalAmount { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public List<PurchaseOrderDetailsVM> PurchaseOrderDetails { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<PurchaseOrderVM,PurchaseOrder>()
                .ForMember(x => x.Date,y => y.MapFrom(obj => obj.PurchaseOrderDate.ToEgyptionDate()))
                .ForMember(x => x.Notes,y => y.MapFrom(obj => PurchaseOrderNotes(obj.Notes)))
                .ForMember(x => x.DeliveryDate,y => y.MapFrom(obj => obj.DeliveryDate.ToEgyptionDate()));
            configuration.CreateMap<PurchaseOrder, PurchaseOrderVM>()
                .ForMember(x => x.PurchaseOrderDate, y => y.MapFrom(obj => obj.Date.ToEgyptianDate()))
                .ForMember(x => x.DeliveryDate, y => y.MapFrom(obj => obj.DeliveryDate.ToEgyptianDate()))
                .ForMember(x => x.Notes,y => y.MapFrom(obj => PurchaseOrderNotes(obj.Notes)))
                .ForMember(x => x.SupplierName, y => y.MapFrom(obj => obj.Supplier.SupplierName??"N/A"));

        }
        public static List<PurchaseOrderNotes> PurchaseOrderNotes(string notes)
        {
            return JsonConvert.DeserializeObject< List<PurchaseOrderNotes>>(notes);
        }
        public static string PurchaseOrderNotes(List<PurchaseOrderNotes> notes)
        {
            return JsonConvert.SerializeObject(notes);
        }
       
    }
    public class PurchaseOrderNotes
    {
        public int Id { get; set; } 
        public string Note { get; set; }
    }
}
