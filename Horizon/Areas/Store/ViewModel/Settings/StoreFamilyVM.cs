using BaseEntities;
using Finance.CurrentAssetModule.Stores.Model.Settings;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Store.ViewModel.Settings
{
    public class StoreFamilyVM:BaseId,IMapFrom<StoreFamily>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string NameAr { get; set; }
    }
}
