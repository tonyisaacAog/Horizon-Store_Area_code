using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Stores.Model.Main
{
    [Table("Finance_CurrentAsset_Stores_Settings_StoreItem")]
    public class StoreItem : BaseEntity
    {
        [Required, StringLength(255)]
        public string ProductName { get; set; }

        // الخاص بالسيستم - هننشأه
        [Required, StringLength(255)]
        public string ProductNameAr { get; set; }


        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public StoreFamily Family { get; set; }

        public int StoreBrandId { get; set; }
        [ForeignKey("StoreBrandId")]
        public StoreBrand StoreBrand { get; set; }

       
        [StringLength(50)]
        public string? ItemCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentQty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DestroyedQty { get; set; }

      

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }


        public void UpdateQTY(decimal value, bool Plus)
        {
            if (Plus)
                CurrentQty += value;
            else
                CurrentQty -= value;
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
