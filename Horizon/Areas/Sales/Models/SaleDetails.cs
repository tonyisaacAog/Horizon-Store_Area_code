using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.Models.Settings;

namespace Horizon.Areas.Purchases.Models
{
    [Table("Finance_PurchasingModule_SaleDetails")]
    public class SaleDetails : BaseEntity
    {
        
        public decimal Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalSales { get; set; }
        public string? Notes { get; set; }
        public DetailType DetailType { get; set; }
        public int? StoreItemId { get; set; }
        [ForeignKey(nameof(StoreItemId))]
        public StoreItem? StoreItem { get; set; }
        public int? StoreItemsRawId { get; set; }
        [ForeignKey(nameof(StoreItemsRawId))]
        public StoreItemsRaw? StoreItemsRaw { get; set; }
        public int SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; }
    }
}