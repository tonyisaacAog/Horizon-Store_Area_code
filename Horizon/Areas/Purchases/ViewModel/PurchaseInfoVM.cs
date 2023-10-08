using Horizon.Areas.Purchases.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyInfrastructure.Mapping;
using AutoMapper;
using MyInfrastructure.Extensions;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class PurchaseInfoVM : IHaveCustomMappings
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string PurchasingDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<PurchaseInfoVM,Purchasing>()
                .ForMember(x=>x.PurchasingDate,y=>y.MapFrom(s=>s.PurchasingDate.ToEgyptionDate()));
            configuration.CreateMap<Purchasing, PurchaseInfoVM>()
                .ForMember(x => x.PurchasingDate, y => y.MapFrom(s => s.PurchasingDate.ToEgyptianDate()));
        }
    }
}
