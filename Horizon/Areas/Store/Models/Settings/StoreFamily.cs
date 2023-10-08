using BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Stores.Model.Settings
{
    [Table("Finance_CurrentAsset_Stores_Settings_StoreFamily")]
    public class StoreFamily : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string NameAr { get; set; }
       
    }
}
