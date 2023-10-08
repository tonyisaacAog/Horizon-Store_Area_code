using Horizon.Areas.Sales.ViewModel;

namespace Horizon.Areas.Orders.ViewModel.Container
{
    public class OrderContainer
    {
        public OrderContainer()
        {
            Order = new();
            Client = new();
            OrderDetail = new();
        }

        public OrderVM Order { get; set; }
        public ClientVM Client { get; set; }
        public List<OrderDetailsVM> OrderDetail { get; set; }
    }
}
