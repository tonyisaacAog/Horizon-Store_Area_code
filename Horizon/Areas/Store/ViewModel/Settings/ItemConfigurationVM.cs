using AutoMapper;
using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Models.Shared;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class ItemConfigurationVM : IHaveCustomMappings
    {
        public int Id { get; set; }
        public int StoreItemId { get; set; }
        public string? StoreItemName { get; set; }
        public int StoreItemRawId { get; set; }
        public string? StoreItemsRawName { get; set; }
        [Required]
        public int MinimumAmount { get; set; }
        public int UsedQTY { get; set; }
        public decimal CurrentQty { get; set; }
        public RecordStatus RecordStatus {get;set;}
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<ItemConfguration, ItemConfigurationVM>()
                .ForMember(x => x.StoreItemName, y => y.MapFrom(c => c.StoreItem.ProductName))
                .ForMember(x => x.StoreItemsRawName, y => y.MapFrom(c => c.StoreItemsRaw.ItemName));
            configuration.CreateMap<ItemConfigurationVM, ItemConfguration>();
        }

    }
}
