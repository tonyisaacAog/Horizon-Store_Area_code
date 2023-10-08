using BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Horizon.Areas.Store.Models.Settings
{
    [Table("Finance_CurrentAssets_StoreSetting_StoreLocations")]
    public class StoreLocations : BaseEntity
    {
        [Required, StringLength(50)]
        [Display(Name = "Store Name")]
        public string LocationName { get; set; }

        [Display(Name = "Main Store")]
        public bool IsDefaultLocation { get; set; }
    }
}
