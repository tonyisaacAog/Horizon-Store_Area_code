using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Orders.ViewModel.Container
{
    public class OrderConfigureContainer
    {
        public OrderConfigureContainer()
        {
            StoreItemVM = new StoreItemVM();
            OrderConfigure = new List<OrderConfigureVM>();
        }
        public int? orderId { get; set; }
        public int OrderDetailId { get; set; }
        public StoreItemVM StoreItemVM { get; set; }
        public List<OrderConfigureVM> OrderConfigure { get; set; }
    }
    public class OrderReportContainerVM
    {

        public OrderReportContainerVM() { StoreItems = new(); OrderConfigures = new();Order = new(); Parameters = new(); }
        public OrderVM Order { get; set; }
        public List<ParameterLstReportVM> Parameters { get; set; }
        public List<StoreItemReportVM> StoreItems { get; set; }
        public List<ItemRawReportVM> OrderConfigures { get; set; }
    }
    public class StoreItemReportVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
    public class ItemRawReportVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }
    public class ParameterLstReportVM
    {
        public string Owner { get; set; }
        public string ReceivedName { get; set; }
        public string NoOrderT { get; set; }
        public string NoOrderF { get; set; }
        public string DateHeader { get; set; }
        public string DateFooter { get; set; }
    }
}
