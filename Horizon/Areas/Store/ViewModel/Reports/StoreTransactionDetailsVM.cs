using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Finance.CurrentAssetModule.Stores.Model.Main;
using MyInfrastructure.Mapping;

namespace Horizon.Areas.Store.ViewModel.Reports
{
    public class StoreTransactionDetailsVM : IHaveCustomMappings
    {
        public int Id { get; set; }
        public int StoreTransId { get; set; }
        public int StoreItemId { get; set; }
        public string StoreItemName { get; set; }
        public int QTY { get; set; }
        public int? QtyAfter { get; set; }
        public decimal UnitPrice { get; set; }
        public string TransDate { get; set; }
        public string TransTypeName { get; set; }
        public int ReferanceId { get; set; }
        public StoreTransTypeEnum TransType { get; set; }
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<StoreTransactionDetails, StoreTransactionDetailsVM>()
               .ForMember(x => x.StoreItemName, y => y.MapFrom(s => s.StoreItem.ProductName))
               .ForMember(x => x.UnitPrice, y => y.MapFrom(s => s.UnitPrice))
               .ForMember(x => x.TransType, y => y.MapFrom(s => s.StoreTransactions.TransType))
               .ForMember(x => x.TransDate, y => y.MapFrom(s => s.StoreTransactions.TransDate));
            configuration.CreateMap<StoreTransactionDetailsVM, StoreTransactionDetails>();
        }
    }
}
