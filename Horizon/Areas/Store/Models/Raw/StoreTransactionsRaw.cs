using BaseEntities;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Store.Models.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.Models.Raw
{
    [Table("Finance_CurrentAssets_Store_StoreTransactionsRaw")]
    public class StoreTransactionsRaw : BaseEntity
    {
        // Shared
        public int StoreItemId { get; set; }
        [ForeignKey("StoreItemId")]
        public StoreItemsRaw StoreItems { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWithrd { get; set; }
        public decimal RestQty { get; set; }
        public decimal DestroyQty { get; set; }
        public bool isEmpty { get; set; }
        public StoreRawTransTypeEnum TransType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal QtyBalanceAfter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountBalanceAfter { get; set; }

        [Column(TypeName = "Date")]
        public DateTime TransDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        [StringLength(500)]
        public string? Descrpition { get; set; }
        //For Purchasing
        public int? PurchaseId { get; set; }
        public int? ManfacturingId { get; set; }
        public int? SaleId { get; set; }

        public void UpdateRestQty(decimal value,bool Plus)
        {
            if (Plus)
            {
                RestQty += value;
            }
            else
            {
                RestQty-=value;
            }
        }
        public void UpdateTotalWithrd(decimal value, bool Plus)
        {
            if (Plus)
            {
                TotalWithrd += value;
            }
            else
            {
                TotalWithrd -= value;
            }
        }
        public void UpdateDestroyAmount(decimal value, bool Plus)
        {
            if (Plus)
            {
                DestroyQty += value;
            }
            else
            {
                DestroyQty -= value;
            }
        }
    }
}
