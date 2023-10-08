using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Horizon.Data;
using Horizon.Areas.Settings.Models;

namespace Horizon.Areas.Settings.Services
{
    public class UserDataManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserDataManager(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _db = db;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

        public ApplicationUser GetCurrentLogedUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.User.Claims.Count() > 0)
                {

                    var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    return _db.ApplicationUsers.Find(userId);
                }
            }

            return null;
        }

        public async Task<bool> CheckUserInClaim(string claimsName)
        {
            var user = GetCurrentLogedUser();
            var UsersInClaim = await _userManager.GetUsersForClaimAsync(new Claim(claimsName, claimsName));
            if (UsersInClaim.Any(x => x.Id == user.Id))
                return true;
            else
                return false;
        }
    }


}
