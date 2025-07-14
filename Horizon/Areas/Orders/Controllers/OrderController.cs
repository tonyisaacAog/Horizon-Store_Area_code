using BoldReports.Web;
using BoldReports.Writer;
using Horizon.Areas.Orders.Models;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Orders.ViewModel;
using Horizon.Areas.Orders.ViewModel.Container;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Orders.Controllers
{
    [Area("Orders")]
  
    public class OrderController : Controller
    {
        private readonly OrderManager _orderManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController(OrderManager orderManager,IWebHostEnvironment webHostEnvironment)
        {
            _orderManager = orderManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(OrderStatus? Status)
        {
            var orders = await _orderManager.GetOrders(Status);
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
            return Redirect("/Orders/Order/Index?Status=" + OrderStatus.Process);
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
            var orderContainer = await _orderManager.NewOrder(Id);
            return View(orderContainer);
        }
        public async Task<IActionResult> ManageOrderForPerson()
        {
            var orderContainer = await _orderManager.NewOrderForPerson();
            return View(orderContainer);
        }

        [HttpPost]
        public async Task<JsonResult> SaveOrder([FromBody] OrderContainer vm)
        {
            var feedback = await _orderManager.SaveOrders(vm);
            if( feedback.Done )
                return Json(new { newLocation = $"/Orders/Order/Index?Status={OrderStatus.New}?success" });
            else
                return Json(new { errors = feedback.Messages });
        }
        [HttpPost]
        public async Task<JsonResult> SaveOrderForPerson([FromBody] OrderForPersonContainer vm)
        {
            var feedback = await _orderManager.SaveOrdersForPerson(vm);
            if (feedback.Done)
                return Json(new { newLocation = $"/Orders/Order/Index?Status={OrderStatus.New}?success" });
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
            if( feedback.Done )
                return Json(new { newLocation = "/Orders/Order/OrderDetails/" + vm.orderId });
            else
                return Json(new { errors = feedback.Messages });
        }


        public async Task<IActionResult> PrintOrder(int id)
        {
            try
            {
                string uploadsFolder = "Areas/Orders/BoldReports/OrderReport.rdlc";
                FileStream inputStream = new FileStream(uploadsFolder,FileMode.Open,FileAccess.Read);

                MemoryStream reportStream = new MemoryStream();
                inputStream.CopyTo(reportStream);
                reportStream.Position = 0;
                inputStream.Close();

                ReportWriter writer = new ReportWriter();

                writer.ExportResources.UsePhantomJS = true;
                writer.ExportResources.IncludeText = true;
                writer.ExportResources.PhantomJSPath = Path.Combine(_webHostEnvironment.WebRootPath,"PhantomJS");
                writer.ExportResources.Scripts = new List<string>
            {
                //Gauge component scripts
                "https://cdn.boldreports.com/5.1.20/scripts/common/ej2-base.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/common/ej2-data.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/common/ej2-pdf-export.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/common/ej2-svg-base.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/data-visualization/ej2-lineargauge.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/data-visualization/ej2-circulargauge.min.js",
                //Map component script
                "https://cdn.boldreports.com/5.1.20/scripts/data-visualization/ej2-maps.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/common/bold.reports.common.min.js",
                "https://cdn.boldreports.com/5.1.20/scripts/common/bold.reports.widgets.min.js",
                //Chart component script
                "https://cdn.boldreports.com/5.1.20/scripts/data-visualization/ej.chart.min.js",
                 //Report Viewer Script
                "https://cdn.boldreports.com/5.1.20/scripts/bold.report-viewer.min.js",
            };
                writer.ExportResources.DependentScripts = new List<string>
            {
                "https://code.jquery.com/jquery-1.10.2.min.js"
            };

                writer.ReportProcessingMode = ProcessingMode.Local;
                writer.DataSources.Clear();

                var Model = await _orderManager.OrderReport(id);

                Model.Parameters.Add(new ParameterLstReportVM
                {
                    NoOrderF = $"ادارة المبيعات QF 8.2.7",
                    NoOrderT = $"نموذج رقم QF 8.2.7",
                    Owner = "امانى",
                    ReceivedName = "ماجد",
                    DateFooter = DateTime.Today.ToString("dd/MM/yyyy"),
                    DateHeader = DateTime.Today.ToString("dd/MM/yyyy"),
                });
              
                writer.DataSources.Add(new ReportDataSource { Name = "ParamterLst",Value = Model.Parameters });
                writer.DataSources.Add(new ReportDataSource { Name = "Order",Value = new List<OrderVM> { Model.Order } });
                writer.DataSources.Add(new ReportDataSource { Name = "StoreItems",Value = Model.StoreItems });
                writer.DataSources.Add(new ReportDataSource { Name = "OrderConfigures",Value = Model.OrderConfigures });
                writer.LoadReport(reportStream);
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream,WriterFormat.PDF);
                memoryStream.Position = 0;
                FileStreamResult fileStreamResult = new FileStreamResult(memoryStream,"application/" + "pdf");
                fileStreamResult.FileDownloadName = $"{Model.Order.NoOfOrder??DateTime.Today.Date.ToString()}.pdf";
                return fileStreamResult;


            }catch(Exception ex )
            {
                throw ex;
            }
        }
    }
}
