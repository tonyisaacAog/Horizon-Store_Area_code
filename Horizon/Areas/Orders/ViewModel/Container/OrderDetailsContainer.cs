namespace Horizon.Areas.Orders.ViewModel.Container
{
    public class OrderDetailsContainer
    {
        public OrderDetailsContainer()
        {
            Order = new();
            OrderDetail = new();
            OrderConfigure = new();
        }

        public OrderVM Order { get; set; }
        public List<OrderDetailsVM> OrderDetail { get; set; }
        public List<OrderConfigureVM> OrderConfigure { get; set; }
    }
}
