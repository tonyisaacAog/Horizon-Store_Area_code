using BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Settings.Models
{
    [Table("Admin_Team")]
    public class Team
    {
        [Required, StringLength(255)]
        public string TeamName { get; set; }
        [Required, StringLength(255)]
        public string TeamManager { get; set; }

        [Required, StringLength(255)]
        public string FinanceManager { get; set; }
    }
}