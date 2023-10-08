using BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.Models.Settings
{
    [Table("Finance_CurrentAssets_Store_RawItemType")]
    public class RawItemType:BaseEntity
    {
        [Required, StringLength(50)]
        [Display(Name = "RawItemType Name")]
        public string RawItemTypeName { get; set; }
    }
}
