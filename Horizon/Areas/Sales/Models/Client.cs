using BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Sales.Models
{
    [Table("Finance_SalesModule_Client")]
    public class Client : BaseEntity
    {
        [Required, StringLength(100)]
        public string ClientName { get; set; }

        [Required, StringLength(100)]
        public string ClientNameAr { get; set; }

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
