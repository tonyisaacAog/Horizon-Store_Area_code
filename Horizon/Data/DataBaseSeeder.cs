using Horizon.Areas.Settings.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EtaMiddleware
{
    public class DataBaseSeeder
    {
        


        public static async Task SeedingRoles(IServiceProvider services)
        {

            using (var scope = services.CreateScope())
            {
                var s = scope.ServiceProvider;


                var userManager = s.GetService<UserManager<ApplicationUser>>();





                var superAdminUser = new ApplicationUser
                {
                    UserName = "horizon",
                    NormalizedUserName = "HORIZON",
                    Email = "horizon@gamil.com",
                    NormalizedEmail = "HORIZON@GAMIL.COM",
                    EmailConfirmed = true,
                };



                var PasswordHasher = new PasswordHasher<ApplicationUser>();
                superAdminUser.PasswordHash = PasswordHasher.HashPassword(superAdminUser, "horizon");


                if (await userManager.FindByNameAsync(superAdminUser.UserName) == null)
                {
                    await userManager.CreateAsync(superAdminUser);
                }

            }
        }

    }
}
