using AutoMapper;
using Horizon.Areas.Sales.Models;
using MyInfrastructure.Extensions;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Sales.ViewModel
{
    public class SaleInfoVM:IHaveCustomMappings
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string SalesDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }
        public int StoreLocationId { get; set; }
        public string? StoreLocationName { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<SaleInfoVM, Sale>()
                .ForMember(x => x.SalesDate, y => y.MapFrom(s => s.SalesDate.ToEgyptionDate()));
            configuration.CreateMap<Sale, SaleInfoVM>()
                .ForMember(x => x.SalesDate, y => y.MapFrom(s => s.SalesDate.ToEgyptianDate()));
        }
    }
}
