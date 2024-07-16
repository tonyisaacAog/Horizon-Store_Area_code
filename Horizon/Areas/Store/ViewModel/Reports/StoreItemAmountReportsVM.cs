using AutoMapper;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Models.Settings;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class StoreItemAmountReportsVM : IHaveCustomMappings
    {
        public int StoreItemId { get; set; }
        public string StoreItemName { get; set; }
        public int DestroyedQty { get; set; } = 0;
        public int CurrentQty { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreItem,StoreItemAmountReportsVM>()
                .ForMember(x => x.StoreItemName,y => y.MapFrom(obj => obj.ProductName ?? obj.ProductNameAr));
        }
    }
    public class StoreItemRawAmountReportsVM : IHaveCustomMappings
    {
        public int StoreItemRawId { get; set; }
        public string StoreItemRawName { get; set; }
        public int DestroyedQty { get; set; } = 0;
        public decimal QTY { get; set; }
        public decimal WarningLimit { get; set; }
        public string StoreItemRawType { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreItemsRaw,StoreItemRawAmountReportsVM>()
                .ForMember(x => x.StoreItemRawName,y => y.MapFrom(obj => obj.ItemName ?? obj.ItemNameAr))
                .ForMember(x => x.StoreItemRawType,y => y.MapFrom(obj => obj.RawItemType.RawItemTypeName));
        }
    }
    public class StoreItemAmountNotCollectReportVM
    {
        public string StoreItemName { get; set; }
        public int StoreItemQuantity { get; set; }
        public decimal PriceItemsRawPurchase { get; set; }
        public decimal PriceItemsRaw { get; set; }
    }

}
