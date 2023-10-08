using Finance.CurrentAssetModule.Stores.Model.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BaseEntities;
using MyInfrastructure.Mapping;
using AutoMapper;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Areas.Store.ViewModel.Main
{
    public class StoreItemVM:BaseId, IHaveCustomMappings
    {
        [Required, StringLength(255)]
        public string ProductName { get; set; }

        // الخاص بالسيستم - هننشأه
        [Required, StringLength(255)]
        public string ProductNameAr { get; set; }
        public int FamilyId { get; set; }
        public string? FamilyName { get; set; }

        public int StoreBrandId { get; set; }
        public string? StoreBrandName { get; set; }
        [StringLength(50)]
        public string? ItemCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentQty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DestroyedQty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public int Quantity { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreItem, StoreItemVM>()
                .ForMember(x => x.StoreBrandName, y => y.MapFrom(b => b.StoreBrand.StoreBrandName))
                .ForMember(x => x.FamilyName, y => y.MapFrom(f => f.Family.Name));
            configuration.CreateMap<StoreItemVM, StoreItem>();
             
        }
    }
}
