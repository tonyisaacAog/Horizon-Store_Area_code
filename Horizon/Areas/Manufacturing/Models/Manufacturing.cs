using BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Models
{
    [Table("Finance_Manufacturing_ManufacturingBatch")]
    public class ManufacturingBatch:BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime BatchDate { get; set; }
        public string? BatchNumber { get; set; }
        
    }
}
