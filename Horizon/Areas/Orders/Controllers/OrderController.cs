using Horizon.Areas.Orders.Models;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Orders.ViewModel;
using Horizon.Areas.Orders.ViewModel.Container;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrderController : Controller
    {
        private readonly OrderManager _orderManager;

        public OrderController(OrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders =await _orderManager.GetOrders();
            return View(orders);
        }
        public async Task<IActionResult> ProcessIndex()
        {
            var orders = await _orderManager.GetProcessOrder();
            return View(orders);
        }

        public async Task<IActionResult> SaveNotesForOrder([FromBody] OrderVM orderVM)
        {
            var fdback = await _orderManager.UpdateNotesOrder(orderVM);
            if( fdback.Done )
                return Json(new { newLocation = "/Orders/Order/OrderDetails/" + orderVM.Id });
            else
                return Json(new { errors = fdback.Messages });
        }
        public async Task<IActionResult> StartProcessOrder(int id)
        {
            await _orderManager.ChangeOrderSatus(id,OrderStatus.Process);
            return Redirect("/Orders/Order/Index");
        }

        public async Task<IActionResult> OrderDetails(int Id)
        {
            var orderContainer = await _orderManager.GetDetails(Id);
            return View(orderContainer);
        }

        public async Task<IActionResult> ProcessOrderDetails(int Id)
        {
            var orderContainer = await _orderManager.GetDetails(Id);
            return View(orderContainer);
        }



        ////////////////////////////////////
        ///       Manage Order ////
        //////////////////////////////////
        public async Task<IActionResult> ManageOrder(int Id)
        {
            var orderContainer =await _orderManager.NewOrder(Id);
            return View(orderContainer);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveOrder([FromBody] OrderContainer vm)
        {
            var feedback = await _orderManager.SaveOrders(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Orders/Order/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }


        ////////////////////////////////////
        ///       Manage Configuration ////
        //////////////////////////////////
        public async Task<IActionResult> ManageConfigure(int Id)
        {
            var orderContainer = await _orderManager.ExtraConfiguration(Id);
            return View(orderContainer);
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SaveExtraConfiguration([FromBody] OrderConfigureContainer vm)
        {
            var feedback = await _orderManager.SaveConfiguration(vm);
            if (feedback.Done)
                return Json(new { newLocation = "/Orders/Order/OrderDetails/"+vm.orderId });
            else
                return Json(new { errors = feedback.Messages });
        }

    }
}
