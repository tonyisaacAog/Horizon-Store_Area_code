using BaseEntities;
using Horizon.Areas.Store.Models.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreLocationsVM:BaseId, IMapFrom<StoreLocations>
    {
        [Required, StringLength(50)]
        [Display(Name = "Store Name")]
        public string LocationName { get; set; }

        [Display(Name = "Main Store")]
        public bool IsDefaultLocation { get; set; }
        //public string SaveUrl { get; set; }
    }
}
