using System.ComponentModel.DataAnnotations;

namespace Horizon.Areas.Settings.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
