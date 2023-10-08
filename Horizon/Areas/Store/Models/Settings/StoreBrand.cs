using BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Stores.Model.Settings
{
    [Table("Finance_CurrentAsset_Stores_Settings_StoreBrand")]

    public class StoreBrand : BaseEntity
    {
        [Required, StringLength(50)]
        public string StoreBrandName { get; set; }

        [Required, StringLength(50)]
        public string StoreBrandNameAr { get; set; }
    }
}
