using AutoMapper;
using Horizon.Areas.Settings.Models;
using Horizon.Areas.Settings.ViewModel;
using Horizon.Data;
using Microsoft.AspNetCore.Identity;
using MyInfrastructure.Extensions;
using MyInfrastructure.Model;

namespace Horizon.Areas.Settings.Services
{
    public class UserManagmentServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public UserManagmentServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _db = db;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public CreateUserViewModel NewUserVM() => new CreateUserViewModel()
        {
            UserRoles = _roleManager.Roles.Select(x => new UserRoleViewModel()
            { Checked = false, RoleName = x.Name }).ToList()
        };


        public async Task<FeedBackWithMessages> SaveNewUser(CreateUserViewModel vm)
        {
            var feedback = new FeedBackWithMessages();
            try
            {
                var user = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = vm.Email,
                    UserName = vm.UserName,
                    Name = vm.Name,
                    PhoneNumber = vm.Phone,
                    Mobile = vm.Mobile
                };

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    foreach (var item in vm.UserRoles)
                    {
                        if (item.Checked)
                            await _userManager.AddToRoleAsync(user, item.RoleName);
                    }
                    feedback.Done = true;
                }
                else
                {
                    feedback.Done = false;
                    feedback.Messages.AddRange(result.Errors.Select(x => x.Description));
                }
                await _db.SaveChangesAsync();

                //feedback.Done = true;
            }
            catch (Exception ex)
            {

                feedback.Done = false;
                feedback.Messages.AddRange(ex.GetExpctionErrors());
            }


            return feedback;
        }



        public async Task<UpdateUserViewModel> GetUserData(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var vm = _mapper.Map<UpdateUserViewModel>(user);
            vm.UserRoles = _roleManager.Roles.Select(x => new UserRoleViewModel()
            {
                Checked = false,
                RoleName = x.Name
            }).ToList();

            foreach (var item in vm.UserRoles)
            {
                var UserInRole = await _userManager.GetUsersInRoleAsync(item.RoleName);

                if (UserInRole.Any(x => x.Id == user.Id))
                {
                    item.Checked = true;
                }
            }
            return vm;
        }


        public async Task<FeedBackWithMessages> UpdateUser(UpdateUserViewModel vm)
        {
            var feedback = new FeedBackWithMessages();

            try
            {

                var user = await _userManager.FindByIdAsync(vm.Id);
                user.Email = vm.Email;
                user.UserName = vm.UserName;
                user.Name = vm.Name;
                user.PhoneNumber = vm.PhoneNumber;
                user.Mobile = vm.Mobile;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    foreach (var item in vm.UserRoles)
                    {
                        var UserInRole = await _userManager.GetUsersInRoleAsync(item.RoleName);

                        if (UserInRole.Any(x => x.Id == user.Id))
                        {
                            if (item.Checked == false)
                                await _userManager
                                    .RemoveFromRoleAsync(user, item.RoleName);
                        }
                        else
                        {
                            if (item.Checked == true)
                                await _userManager
                                    .AddToRoleAsync(user, item.RoleName);
                        }
                    }
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


        public async Task<ResetPasswordViewModel> ResetPasswordVM(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var vm = new ResetPasswordViewModel();
            vm.UserId = userId;
            vm.UserName = user.UserName;
            return vm;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, vm.Password);

            return await _userManager.AddPasswordAsync(user, vm.Password);
        }

        public async Task<IdentityResult> LockUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            DateTimeOffset da = DateTimeOffset.Now.AddYears(1000);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, da);
                return result;
            }
            else
            {
                throw new InvalidOperationException("User Not Exist");
            }
        }

        public async Task<IdentityResult> UnLockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, null);
                return result;

            }
            else
            {
                throw new InvalidOperationException("User Not Exist");
            }

        }
    }
}
