using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.ViewModel.Transaction
{
    public class SaleStoreTransactionVM:IHaveCustomMappings
    {
        public int Id { get; set; }
        public int StoreTransId { get; set; }
        public int StoreItemId { get; set; }
        public string StoreItemName { get; set; }
        public int QTY { get; set; }
        public int? QtyBalanceAfterSource { get; set; }
        public int? QtyBalanceAfterDestination { get; set; }
        public decimal? AmountBalanceAfter { get; set; }
        public decimal UnitPrice { get; set; }
        public StoreTransTypeEnum TransType { get; set; } = StoreTransTypeEnum.Sale;
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreTransactionDetails, SaleStoreTransactionVM>()
               .ForMember(x => x.StoreItemName, y => y.MapFrom(s => s.StoreItem.ProductName))
               .ForMember(x => x.UnitPrice, y => y.MapFrom(s => s.UnitPrice));
            configuration.CreateMap<SaleStoreTransactionVM, StoreTransactionDetails>();
        }
    }
}
