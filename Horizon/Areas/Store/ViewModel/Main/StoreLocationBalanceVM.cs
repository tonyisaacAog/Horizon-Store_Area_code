using AutoMapper;
using Finance.CurrentAssetModule.Stores.Model.Main;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Store.ViewModel.Main
{
    public class StoreLocationBalanceVM:IHaveCustomMappings
    {
        public int LocationId { get; set; }

        public string? LocationName { get; set; }

        public int StoreItemId { get; set; }

        public string?  StoreItemName { get; set; }
        public int DestroyedQty { get; set; }
        public int QTY { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreLocationsBalance, StoreLocationBalanceVM>()
                .ForMember(x => x.LocationName, y => y.MapFrom(obj => obj.Locations.LocationName))
                .ForMember(x => x.StoreItemName, y => y.MapFrom(obj => obj.StoreItemDetails.ProductName));
            configuration.CreateMap<StoreLocationBalanceVM, StoreLocationsBalance>();
        }
    }
}
