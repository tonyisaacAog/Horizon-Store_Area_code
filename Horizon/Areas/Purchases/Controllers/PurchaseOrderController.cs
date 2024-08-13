using BoldReports.Web;
using BoldReports.Writer;
using Horizon.Areas.Purchases.BoldReports;
using Horizon.Areas.Purchases.Services;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Microsoft.AspNetCore.Mvc;
using MyInfrastructure.Filters;

namespace Horizon.Areas.Purchases.Controllers
{
    [Area("Purchases")]
    public class PurchaseOrderController : Controller
    {
        public readonly PurchaseOrderManager _purchaseOrderManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PurchaseOrderController(PurchaseOrderManager purchaseOrderManager,IWebHostEnvironment webHostEnvironment)
        {
            _purchaseOrderManager = purchaseOrderManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var purchaseOrder = await _purchaseOrderManager.GetAll();
            return View(purchaseOrder);
        }

        public async Task<IActionResult> ManagePurchaseOrder(int id)
        {
            if( id > 0 )
            {
                var details = await _purchaseOrderManager.PurchaseOrderDetails(id);
                if( details == null )
                {
                    return Redirect("/Purchases/PurchaseOrder/Index");
                }
                if( details.IsStoreInStock == true || details.PurchaseOrderDetails.Any(obj => obj.IsCreatedASPurchasing == true) )
                { return View("Details",details); }
                return View(details);
            }
            return View(new PurchaseOrderVM());
        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<JsonResult> SavePurchaseOrder([FromBody] PurchaseOrderVM vm)
        {
            var feedback = await _purchaseOrderManager.SavePurchaseOrder(vm);
            if( feedback.Done )
                return Json(new { newLocation = $"/Purchases/PurchaseOrder/Index?success" });
            else
                return Json(new { errors = feedback.Messages });
        }

        public async Task<IActionResult> PrintPurchaseOrder(int id)
        {
            try
            {
                string uploadsFolder = "Areas/Purchases/BoldReports/PurchaseOrder.rdlc";
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

                var Model = await _purchaseOrderManager.PurchaseOrderDetails(id);

                var PurchaseOrderDetails = Model.PurchaseOrderDetails.Select((obj,index) => new PurchaseOrderDetailsReports { Id = index + 1,ItemName = obj.StoreItemName,Notes = obj.Notes,Quantity = obj.StoreItemAmount });
                var PurchaseOrderNotes = Model.Notes.Select((obj,index) => new PurchaseOrderNotesReports { Id = index,Note = obj.Note });
                var paramters = new PurchaseOrderParamsReport { DeliveryDate = Model.DeliveryDate,Name = "",NoPurchaseOrder = $"امر انتاج رقم ( {Model.PurchaseOrderNumber} )",PurchaseOrderDate = Model.PurchaseOrderDate,Recevier = $"السادة / {Model.SupplierName}",Signature = "",NoNotes = PurchaseOrderNotes.Count() == 0? true:false };
                writer.DataSources.Add(new ReportDataSource { Name = "Parameters",Value = new List<PurchaseOrderParamsReport> { paramters } });
                writer.DataSources.Add(new ReportDataSource { Name = "PurchaseOrderDetails",Value = PurchaseOrderDetails });
                writer.DataSources.Add(new ReportDataSource { Name = "PurchaseOrderNotes",Value = PurchaseOrderNotes });
                writer.LoadReport(reportStream);
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream,WriterFormat.PDF);
                memoryStream.Position = 0;
                FileStreamResult fileStreamResult = new FileStreamResult(memoryStream,"application/" + "pdf");
                fileStreamResult.FileDownloadName = $"{Model.PurchaseOrderNumber ?? DateTime.Today.Date.ToString()}.pdf";
                return fileStreamResult;


            }
            catch( Exception ex )
            {
                throw ex;
            }
        }
    }
}
