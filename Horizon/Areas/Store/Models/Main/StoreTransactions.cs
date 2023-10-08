using BaseEntities;
using Finance.CurrentAssetModule.Store.Model.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.CurrentAssetModule.Store.Model.Main
{
    [Table("Finance_CurrentAssets_Store_StoreTransactions")]
    public class StoreTransactions :BaseEntity
    {
        // Shared
        public int SourceId { get; set; }
        public StoreTransSourceTypes SourceType { get; set; }
        public int DestinationId { get; set; }
        public StoreTransSourceTypes DestinationType { get; set; }
        [Column(TypeName = "Date")]
        public DateTime TransDate { get; set; }
        public StoreTransTypeEnum TransType { get; set; }
        
        [StringLength(500)]
        public string Descrpition { get; set; }
        public int? StoreLocationId { get; set; }


    }
}
