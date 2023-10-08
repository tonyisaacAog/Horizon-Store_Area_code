using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        [StringLength(255)]
        public string? CreatedBy { get; set; }
        [StringLength(255)]
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
