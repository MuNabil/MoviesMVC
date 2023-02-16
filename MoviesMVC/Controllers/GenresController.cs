namespace MoviesMVC.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GenresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return View(genres);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.ItemName = model.ItemName.Trim();
            if (await _unitOfWork.Genres.FindAsync(g => g.GenreName.ToLower() == model.ItemName.ToLower()) is not null)
            {
                TempData["CreateError"] = "This Genre is already exists.!";
                return RedirectToAction(nameof(Index));
            }

            await _unitOfWork.Genres.AddAsync(new Genre { GenreName = model.ItemName, IsApproved = true });

            if (await _unitOfWork.Commit())
                return RedirectToAction(nameof(Index));

            ViewBag.CreateError = "Please Try again.!";
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre is not null)
            {
                _unitOfWork.Genres.Delete(genre);
                await _unitOfWork.Commit();
            }
            else ViewBag.CreateError = "Please Try again.!";

            return RedirectToAction(nameof(Index));
        }
    }
}