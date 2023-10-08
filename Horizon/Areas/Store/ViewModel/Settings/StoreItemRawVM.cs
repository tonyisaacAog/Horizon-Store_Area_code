using Horizon.Areas.Store.Models.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MyInfrastructure.Mapping;
using AutoMapper;
using BaseEntities;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreItemRawVM :BaseId, IHaveCustomMappings
    {
        [Required, StringLength(75)]
        public string ItemName { get; set; }

        [Required, StringLength(75)]
        public string ItemNameAr { get; set; }
        [Required]
        public int RawItemTypeId { get; set; }
        public string? RawItemTypeName { get; set; }

        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal QTY { get; set; }

        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DestroyedQty { get; set; }


        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WarningLimit { get; set; }
        public string? RedirectUrl { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreItemsRaw, StoreItemRawVM>()
                .ForMember(x => x.RawItemTypeName, y => y.MapFrom(s => s.RawItemType.RawItemTypeName));
            configuration.CreateMap<StoreItemRawVM, StoreItemsRaw>();
        }
    }
}
