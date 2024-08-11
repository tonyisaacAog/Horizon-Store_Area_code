using BaseEntities;
using Horizon.Areas.Purchases.Models;
using MyInfrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Purchases.ViewModel
{
    public class SupplierVM :BaseId,IMapFrom<Supplier>
    {
        
        [Required, StringLength(100)]
        public string SupplierName { get; set; } 

        [Required, StringLength(100)]
        public string SupplierNameAr { get; set; }

        [StringLength(50),
            RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]

        public string Phone1 { get; set; }

        [StringLength(50),
            RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string? Phone2 { get; set; }

        [StringLength(50),
            RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string? Phone3 { get; set; }

        [StringLength(50), DataType(DataType.EmailAddress)]

        public string? Email { get; set; }
        
    }
}
