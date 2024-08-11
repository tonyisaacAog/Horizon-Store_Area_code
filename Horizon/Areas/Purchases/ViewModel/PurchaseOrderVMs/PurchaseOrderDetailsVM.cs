using AutoMapper;
using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Purchases.Models;
using Horizon.Models.Shared;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs
{
    public class PurchaseOrderDetailsVM : BaseId, IHaveCustomMappings
    {
        public decimal StoreItemAmount { get; set; }
        public string? Notes { get; set; }
        public bool IsCreatedASPurchasing { get; set; } = false;
        public int StoreItemId { get; set; }
        public int PurchaseOrderId { get; set; }
        public string? StoreItemName { get; set; }
        public RecordStatus RecordStatus { get; set; } = RecordStatus.UnChanged;
        public void CreateMapping(Profile configuration)
        {
            configuration.CreateMap<PurchaseOrderDetailsVM, PurchaseOrderDetails>();
            configuration.CreateMap<PurchaseOrderDetails,PurchaseOrderDetailsVM>()
                .ForMember(x=>x.StoreItemName ,y=>y.MapFrom(obj=>obj.StoreItem.ProductNameAr?? obj.StoreItem.ProductName));
        }
    }
}
