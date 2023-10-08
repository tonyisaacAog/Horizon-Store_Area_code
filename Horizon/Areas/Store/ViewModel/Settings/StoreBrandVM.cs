using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreBrandVM:BaseId,IMapFrom<StoreBrand>
    {
        [Required, StringLength(50)]
        public string StoreBrandName { get; set; }

        [Required, StringLength(50)]
        public string StoreBrandNameAr { get; set; }
    }
}
