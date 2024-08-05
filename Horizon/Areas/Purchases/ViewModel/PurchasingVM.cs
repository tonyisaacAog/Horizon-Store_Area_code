using AutoMapper;
using Horizon.Areas.Purchases.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class PurchasingVM:IHaveCustomMappings
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }

        public decimal TotalAmount { get; set; }

        public string? PurchasingDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }
        public int AmountStoreItem { get; set; }


        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<Purchasing, PurchasingVM>()
                .ForMember(x => x.SupplierName, y => y.MapFrom(s => s.Supplier.SupplierName))
                .ForMember(x => x.PurchasingDate, y => y.MapFrom(s => s.PurchasingDate.ToEgyptianDate()));
            configuration.CreateMap<PurchasingVM, Purchasing>()
                .ForMember(x => x.PurchasingDate, y => y.MapFrom(s => s.PurchasingDate.ToEgyptionDate()));
        }
    }
}
