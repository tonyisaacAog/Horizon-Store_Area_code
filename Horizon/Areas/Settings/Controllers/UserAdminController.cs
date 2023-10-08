using Horizon.Areas.Settings.Services;
using Horizon.Areas.Settings.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Settings.Controllers
{
    [Area("Settings")]
    //[Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private readonly UserManagmentServices _userManagmentServices;
        private readonly RoleManagmentServices _roleManagmentServices;

        public UserAdminController(UserManagmentServices userManagmentServices,
            RoleManagmentServices roleManagmentServices)
        {
            _userManagmentServices = userManagmentServices;
            _roleManagmentServices = roleManagmentServices;
        }
        public IActionResult UserManagment()
        {
            return View(_userManagmentServices.GetAllUsers());
        }

        public IActionResult CreateUser()
        {
            return View(_userManagmentServices.NewUserVM());
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<IActionResult> SaveNewUser([FromBody] CreateUserViewModel vm)
        {
           var feedback =  await _userManagmentServices.SaveNewUser(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Settings/UserAdmin/UserManagment?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await _userManagmentServices.GetUserData(Id);
            return View(user);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<IActionResult> SaveUpdateUser([FromBody] UpdateUserViewModel vm)
        {
            var feedback = await _userManagmentServices.UpdateUser(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Settings/UserAdmin/UserManagment?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

        public IActionResult RoleManagement()
        {
            return View(_roleManagmentServices.GetAllRole());
        }

        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel addRoleViewModel)
        {

            if (!ModelState.IsValid) return View(addRoleViewModel);
            var result = await _roleManagmentServices.AddNewRole(addRoleViewModel);
            if (result.Done)
            {
                return RedirectToAction("RoleManagement");
            }

            foreach (var error in result.Messages)
            {
                ModelState.AddModelError("", error);
            }
            return View(addRoleViewModel);
        }


        public async Task<IActionResult> ResetPassword(string Id)
        {
            var user = await _userManagmentServices.ResetPasswordVM(Id);
            return View(user);
        }

        [HttpPost]
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {

            await _userManagmentServices.ResetPassword(vm);

            return RedirectToAction(nameof(this.UserManagment));
        }

        public async Task<IActionResult> LockUser(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
                await _userManagmentServices.LockUser(Id);

            return View(nameof(this.UserManagment), _userManagmentServices.GetAllUsers());
        }


        public async Task<IActionResult> UnLockUser(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
                await _userManagmentServices.UnLockUser(Id);

            return View(nameof(this.UserManagment), _userManagmentServices.GetAllUsers());
        }
    }
}
