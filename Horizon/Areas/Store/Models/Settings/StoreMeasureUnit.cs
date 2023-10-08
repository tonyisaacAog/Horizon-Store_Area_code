using BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Horizon.Areas.Store.Models.Settings
{
    [Table("Finance_CurrentAssets_StoreSetting_StoreMeasureUnit")]
    public class StoreMeasureUnit : BaseEntity
    {

        [Required, StringLength(50)]
        [Display(Name = "وحدات القياس")]
        public string MeasureUnitName { get; set; }
        [StringLength(5)]
        public string? ECode { get; set; }
    }
}
