namespace MoviesMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int currentPage = 1, string term = "")
        {
            PaginationParams info = new PaginationParams
            {
                PageNumber = currentPage,
                PageSize = 4
            };
            if (string.IsNullOrEmpty(term)) term = "";
            TempData["myterm"] = term;
            var movies = await _unitOfWork.Movies.GetPageListAsync(info,
             m => m.Title.ToLower().Contains(term.Trim().ToLower()) && m.IsApproved, new[] { "Genres.Genre" });
            return View(movies);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}