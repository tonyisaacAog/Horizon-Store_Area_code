using Horizon.Areas.Store.Models.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Stores.Model.Main
{
    [Table("Finance_CurrentAssets_Store_StoreLocationsBalance")]
    public class StoreLocationsBalance
    {
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public StoreLocations Locations { get; set; }
        public int StoreItemId { get; set; }
        [ForeignKey("StoreItemId")]
        public StoreItem StoreItemDetails { get; set; }
        public int DestroyedQty { get; set; } = 0;
        public int QTY { get; set; }
        public void UpdateQTY(int TransQty,bool Plus)
        {
            if( Plus )
                QTY = QTY + TransQty;
            else
            {
                QTY = QTY - TransQty;
                if( QTY < 0 )
                {
                    throw new Exception("الكمية المطلوبة من المنتج غير متاحة فى المخزن");
                }
            }
        }
        public void UpdateDestroyedQTY(int TransQty,bool Plus)
        {
            if( Plus )
                DestroyedQty = DestroyedQty + TransQty;
            else
                DestroyedQty = DestroyedQty - TransQty;
        }
    }
}
