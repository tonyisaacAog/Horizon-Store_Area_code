using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyInfrastructure.Mapping;
using AutoMapper;
using Horizon.Areas.Sales.Models;
using MyInfrastructure.Extensions;
using Horizon.Areas.Purchases.ViewModel;

namespace Horizon.Areas.Sales.ViewModel
{
    public class SaleVM:IHaveCustomMappings
    {
        public int Id { get; set; }
        public string ClientName { get; set; }

        public decimal TotalAmount { get; set; }

        public string? SalesDate { get; set; }
        [StringLength(50)]
        public string? InvoiceNum { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<Sale, SaleVM>()
                .ForMember(x => x.ClientName, y => y.MapFrom(s => s.Client.ClientName))
                .ForMember(x => x.SalesDate, y => y.MapFrom(s => s.SalesDate.ToEgyptianDate()));
            configuration.CreateMap<SaleVM, Sale>()
                .ForMember(x => x.SalesDate, y => y.MapFrom(s => s.SalesDate.ToEgyptionDate()));
        }
    }
}
