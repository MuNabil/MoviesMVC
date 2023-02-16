namespace MoviesMVC.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> FilterApprovedOrders(bool? isApproved = null);
        Task<Order?> GetOrderByIdAsync(int movieId, string userId);
    }
}