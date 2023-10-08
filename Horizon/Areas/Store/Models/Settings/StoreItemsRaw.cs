using BaseEntities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.Models.Settings
{
    [Table("Finance_CurrentAssets_Store_StoreItemsRaw")]
    public class StoreItemsRaw : BaseEntity
    {
        [Required, StringLength(75)]
        public string ItemName { get; set; }

        [Required, StringLength(75)]
        public string ItemNameAr { get; set; }

        public int RawItemTypeId { get; set; }
        [ForeignKey("RawItemTypeId")]
        public RawItemType RawItemType { get; set; }

        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal QTY { get; set; }

        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DestroyedQty { get; set; }

        [DefaultValue(0)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WarningLimit { get; set; }

        public void UpdateQTY(decimal value, bool Plus)
        {

            if (Plus)
                QTY += value;
            else
                QTY -= value;
            if (QTY<0)
            {
                throw new Exception("المواد الخام لا تكفى لعملية التصنيع");
            }
        }

        public void UpdateBalance(decimal value, bool Plus)
        {
            if (Plus)
                Balance += value;
            else
                Balance -= value;
        }
        public void UpdateDestroyedQty(decimal value, bool Plus)
        {
            if (Plus)
                DestroyedQty += value;
            else
                DestroyedQty -= value;
        }


    }


}
