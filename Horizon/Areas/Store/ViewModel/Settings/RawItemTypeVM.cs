using BaseEntities;
using Horizon.Areas.Store.Models.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class RawItemTypeVM:BaseId,IMapFrom<RawItemType>
    {
        [StringLength(50)]
        [Required,Display(Name = "RawItemType Name")]
        public string RawItemTypeName { get; set; }
        //public string SaveUrl { get; set; }

    }
}
