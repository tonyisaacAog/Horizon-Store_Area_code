
using BoldReports.Web;
using BoldReports.Writer;
using Horizon.Areas.Orders.ViewModel;
using Horizon.Areas.Orders.ViewModel.Container;
using Horizon.Areas.Purchases.BoldReports;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Horizon.Areas.Sales.Services;
using Horizon.Areas.Sales.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class HomeController : Controller
    {

        private readonly SalesManager _SalesManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(SalesManager salesManager,IWebHostEnvironment webHostEnvironment)
        {
            _SalesManager = salesManager;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<IActionResult> Index()
        {
            var PurchasesList = await _SalesManager.GetAll();
            return View(PurchasesList);
        }

        public async Task<IActionResult> ManageSales(int Id)
            => View(await _SalesManager.NewSales(Id));

        public async Task<IActionResult> ManageSalesForOrder(int Id)
        {
            var vm = await _SalesManager.NewSalesForOrder(Id);
            vm.IsSaleFromOrder = true;
            vm.OrderId = Id;
            return View(vm);
        }

        public async Task<IActionResult> DetailsSales(int Id)
        {
            var salesDetails = await _SalesManager.DetailsSales(Id);
            return View(salesDetails);
        }

        [HttpPost]
        public async Task<JsonResult> SaveSales([FromBody] SalesContainer vm)
        {
            var feedback = await _SalesManager.SaveSales(vm);
            if( feedback.Done )
                return Json(new { newLocation = "/Sales/Home/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }
        public async Task<IActionResult> PrintSalesInvoice(int id)
        {
            try
            {
                string uploadsFolder = "Areas/Sales/BoldReports/SaleInvoice.rdlc";
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
                var Model = await _SalesManager.DetailsSales(id);
                var data = Model.SaleDetails.Select(obj => new SaleInvoiceRdlcVM
                {
                    Description = string.Empty,
                    Quantity = obj.QTY,
                    Unit = obj.UnitPrice,
                    PN = obj.StoreItemName
                   
                }).ToList();
                data.AddRange(Model.SaleItemRawDetails.Select(obj => new SaleInvoiceRdlcVM
                {
                    Description = string.Empty,
                    Quantity = obj.QTY,
                    Unit = obj.UnitPrice,
                    PN = obj.StoreItemName

                }).ToList());

                var Parameters = new List<PurchaseOrderParamsReport>{new PurchaseOrderParamsReport
                {
                    DeliveryDate = Model.SaleInfo.SalesDate,
                    Name = Model.Client.ClientName,
                    NoPurchaseOrder = Model.SaleInfo.InvoiceNum,
                    PurchaseOrderDate = Model.SaleInfo.SalesDate,
                    Signature = "",
                } };

                writer.DataSources.Add(new ReportDataSource { Name = "ParamterLst",Value = Parameters });
                writer.DataSources.Add(new ReportDataSource { Name = "Invoice",Value = data });
                writer.LoadReport(reportStream);
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream,WriterFormat.PDF);
                memoryStream.Position = 0;
                FileStreamResult fileStreamResult = new FileStreamResult(memoryStream,"application/" + "pdf");
                fileStreamResult.FileDownloadName = $"{Model.SaleInfo.InvoiceNum ?? DateTime.Today.Date.ToString()}.pdf";
                return fileStreamResult;
            }
            catch( Exception ex )
            {
                throw ex;
            }
        }


    }
}
