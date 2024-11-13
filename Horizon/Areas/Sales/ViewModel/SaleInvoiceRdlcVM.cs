namespace Horizon.Areas.Sales.ViewModel
{
    public class SaleInvoiceRdlcVM
    {
        public int No { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Unit { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PN { get; set; } = string.Empty;
    }
}
