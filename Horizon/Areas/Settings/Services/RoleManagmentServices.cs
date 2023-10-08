using Horizon.Areas.Settings.ViewModel;
using Microsoft.AspNetCore.Identity;
using MyInfrastructure.Extensions;
using MyInfrastructure.Model;

namespace Horizon.Areas.Settings.Services
{
    public class RoleManagmentServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagmentServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> GetAllRole() => _roleManager.Roles;

        public async Task<FeedBackWithMessages> AddNewRole(AddRoleViewModel vm)
        {
            var feedback = new FeedBackWithMessages();
            try
            {
                var role = new IdentityRole
                {
                    Name = vm.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    feedback.Done = true;
                }
                else
                {
                    feedback.Done = false;
                    feedback.Messages.AddRange(result.Errors.Select(x => x.Description));
                }
            }
            catch (Exception ex)
            {

                feedback.Done = false;
                feedback.Messages.AddRange(ex.GetExpctionErrors());
            }

            return feedback;
        }
    }
}
