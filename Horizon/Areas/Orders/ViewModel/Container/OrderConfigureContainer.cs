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
}
