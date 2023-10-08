using Microsoft.Build.Framework;

namespace Horizon.Areas.Settings.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
