namespace MoviesMVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly UserManager<AppUser> _userManager;
        public OrdersController(IUnitOfWork unitOfWork, IHubContext<OrderHub> orderHub, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _orderHub = orderHub;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Order>? orders = await _unitOfWork.Orders.GetAllAsync(new[] { "Movie", "User" });
            IEnumerable<OrderVM>? nonApprovedOrders = FillOrderVM(orders!.Where(x => !x.IsApproved));
            IEnumerable<OrderVM>? allOrders = FillOrderVM(orders!);
            FilterOrderVM model = new()
            {
                AllOrders = allOrders,
                NonApprovedOrders = nonApprovedOrders
            };
            return View(model);
        }

        private static IEnumerable<OrderVM>? FillOrderVM(IEnumerable<Order> orders)
        {
            return orders?.Select(order => new OrderVM
            {
                MovieTitle = order.Movie!.Title,
                MovieId = order.MovieId,
                MoviePoster = order.Movie.Poster,
                UserEmail = order.User!.Email,
                UserId = order.UserId,
                UserName = order.User.Name
            });
        }

        public async Task<IActionResult> ApproveOrder(int movieId, string userId)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(movieId, userId);
            if (order is null)
            {
                TempData["errorMessage"] = "Please try again.!";
                return RedirectToAction(nameof(Index));
            }
            order.IsApproved = true;
            order.ApprovedDate = DateTime.Now;
            await _unitOfWork.Commit();

            var movie = await _unitOfWork.Movies.FindAsync(x => x.MovieId == movieId, new[] { "Genres.Genre", "Members.Member" });

            await _orderHub.Clients.User(userId)
                .SendAsync("OrderApproved", movie!.Poster, movie.Title, movie.ReleaseYear,
                 movie.Genres!.Select(x => x.Genre!.GenreName).ToList(),
                  movie.Members!.Select(x => x.Member!.MemberName).ToList());

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Order(int movieId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == User.GetEmail());
            if (await _unitOfWork.Orders.GetOrderByIdAsync(movieId, user!.Id) is not null)
                return RedirectToAction("Index", "Home");

            var order = new Order
            {
                MovieId = movieId,
                UserId = user!.Id,
                OrderDate = DateTime.Now
            };
            var movie = await _unitOfWork.Movies.GetByIdAsync(movieId);
            await _unitOfWork.Orders.AddAsync(order);

            await _unitOfWork.Commit();

            await _orderHub.Clients.User(_userManager.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == "admin@test.com").Result!.Id)
                .SendAsync("NewOrderPlaced", movie!.Poster, movie.Title, user.Name, user.Email, order.OrderDate, movieId, user.Id);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserOrders()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == User.GetEmail());
            var orders = await _unitOfWork.Orders.FindAllAsync(x => x.UserId == user!.Id && x.IsApproved, new[] { "Movie", "Movie.Members.Member", "Movie.Genres.Genre" });
            IEnumerable<Movie?> movies = orders!.Select(x => x.Movie);
            return View(movies);
        }
    }
}