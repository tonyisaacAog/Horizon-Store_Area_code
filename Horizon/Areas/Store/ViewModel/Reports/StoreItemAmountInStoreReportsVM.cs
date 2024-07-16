using AutoMapper;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Models.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class StoreItemAmountInStoreReportsVM : IHaveCustomMappings
    {
        public int LocationId { get; set; }
        public string StoreName { get; set; }
        public int StoreItemId { get; set; }
        public string StoreItemName { get; set; }
        public int DestroyedQty { get; set; } = 0;
        public int QTY { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreLocationsBalance,StoreItemAmountInStoreReportsVM>()
                .ForMember(x => x.StoreItemName,y => y.MapFrom(obj => obj.StoreItemDetails.ProductName ?? obj.StoreItemDetails.ProductNameAr))
                .ForMember(x => x.StoreName,y => y.MapFrom(obj => obj.Locations.LocationName));

        }
    }

}
