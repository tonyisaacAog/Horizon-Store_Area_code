using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Main;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.Models.Settings
{
    [Table("Finance_CurrentAssets_Store_ItemConfguration")]
    public class ItemConfguration:BaseEntityStringKey
    {

        [ForeignKey(nameof(StoreItem))]
        public int StoreItemId { get; set; }
        public StoreItem StoreItem { get; set; }
        [ForeignKey(nameof(StoreItemsRaw))]
        public int StoreItemRawId { get; set; }
        public StoreItemsRaw StoreItemsRaw { get; set; }
        [Required]
        public int MinimumAmount { get; set; }

    }
}
