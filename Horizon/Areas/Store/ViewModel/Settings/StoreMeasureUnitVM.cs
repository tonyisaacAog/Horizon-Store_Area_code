using BaseEntities;
using Horizon.Areas.Store.Models.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreMeasureUnitVM:BaseId, IMapFrom<StoreMeasureUnit>
    {
        [Required, StringLength(50)]
        [Display(Name = "وحدات القياس")]
        public string MeasureUnitName { get; set; }
        [StringLength(5)]
        public string? ECode { get; set; }
        //public string SaveUrl { get; set; }
    }
}
