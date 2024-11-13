using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Store.Models.Raw;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Store.ViewModel.Transaction
{
    public class PurchaseStoreTransactionVM:IHaveCustomMappings
    {
        public int Id { get; set; }
        public int StoreItemId { get; set; }
        public int ConfigueQty { get; set; } = 0;
        public string StoreItemName { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public StoreRawTransTypeEnum TransType { get; set; } = StoreRawTransTypeEnum.Purchase;
        public decimal UnitPrice { get; set; }

        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<PurchaseStoreTransactionVM,StoreTransactionsRaw>();
            configuration.CreateMap<StoreTransactionsRaw, PurchaseStoreTransactionVM>()
                .ForMember(x=>x.StoreItemName,y=>y.MapFrom(s=>s.StoreItems.ItemName))
                .ForMember(x => x.UnitPrice, y => y.MapFrom(s => s.UnitPrice.HasValue?s.UnitPrice.Value:0));



        }
    }

}
