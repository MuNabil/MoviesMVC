namespace MoviesMVC.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> FilterApprovedOrders(bool? isApproved = null)
        {
            if (isApproved is null) return await _context.Orders!.ToListAsync();
            return await _context.Orders!.Include(x => x.User).Include(x => x.Movie)
                .Where(o => o.IsApproved == isApproved).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int movieId, string userId)
        {
            return await _context.Orders!.FindAsync(movieId, userId);
        }
    }
}