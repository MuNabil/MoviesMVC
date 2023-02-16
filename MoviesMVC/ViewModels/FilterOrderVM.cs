namespace MoviesMVC.ViewModels
{
    public class FilterOrderVM
    {
        public IEnumerable<OrderVM>? NonApprovedOrders { get; set; }
        public IEnumerable<OrderVM>? AllOrders { get; set; }
    }
}