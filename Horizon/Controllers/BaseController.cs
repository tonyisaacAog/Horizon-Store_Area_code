using BaseEntities;
using Horizon.Areas.Purchases.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;
using Services;

namespace Horizon.Controllers
{
    public class BaseController<TModel, TViewModel> : Controller where TModel : BaseEntity 
        where TViewModel : BaseId
    {
        protected readonly string _saveUrl;
        protected readonly GenericSettingsManager<TModel, TViewModel> _settingsManager;
        protected readonly string _IndexUrl;

        public BaseController(GenericSettingsManager<TModel, TViewModel> settingsManager, 
            string SaveUrl,
            string IndexUrl)
        {
            _saveUrl = SaveUrl;
            _settingsManager = settingsManager;
            _IndexUrl = IndexUrl;
        }
        public virtual async Task<IActionResult> Index()
        {
            return View(await _settingsManager.GetAll());
        }


        public virtual async Task<IActionResult> ManageRecord(int Id)
        {

            if (Id == 0)
            {
                var vm = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
                vm.SaveUrl = _saveUrl;
                return View(vm);
            }
            else
            {
                var vm = await _settingsManager.CheckAndReturn(Id);
                vm.SaveUrl = _saveUrl;
                return View(vm);
            }

        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveRecord([FromBody] TViewModel vm)
        {
            var feedback = await _settingsManager.SaveManagerData(vm);
            if (feedback.Done)
                return Json(new { newLocation = _IndexUrl });
            else
                return Json(new { errors = feedback.Messages });
        }
    }
}
