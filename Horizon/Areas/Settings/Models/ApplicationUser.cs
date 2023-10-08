using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Settings.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public string? Mobile { get; set; }


        #region
        //public int? TeamId { get; set; }
        //[ForeignKey("TeamId")]
        //public Team Team { get; set; }
        //public bool IsSales { get; set; }
        //public bool IsTeamManager { get; set; }
        //public bool IsFinanceManager { get; set; }
        #endregion

    }
}
