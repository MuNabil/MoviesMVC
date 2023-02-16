using System.Security.AccessControl;
namespace MoviesMVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int currentPage = 1, string term = "")
        {
            PaginationParams info = new PaginationParams
            {
                PageNumber = currentPage,
                PageSize = 9
            };
            if (string.IsNullOrEmpty(term)) term = "";
            TempData["term"] = term;
            var movies = await _unitOfWork.Movies.GetPageListAsync(info,
             m => m.Title.ToLower().Contains(term.Trim().ToLower()) && m.IsApproved,
              new[] { "Genres.Genre", "Members.Member" });
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var movies = await _unitOfWork.Movies.FindAsync(m => m.MovieId == id, new[] { "Genres.Genre", "Members.Member" });
            return View(movies);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie is not null && ImageService.DeleteImage(movie.Poster))
            {
                _unitOfWork.Movies.Delete(movie);
                await _unitOfWork.Commit();
            }

            else ViewBag.CreateError = "Please Try again.!";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new EditMovieVM()
            {
                Genres = _unitOfWork.Genres.GetAllAsync().Result!.Select(g => new CheckboxVM { ItemId = g.GenreId, ItemName = g.GenreName }).ToList(),
                Members = _unitOfWork.Members.GetAllAsync().Result!.Select(m => new CheckboxVM { ItemId = m.MemberId, ItemName = m.MemberName, ItemImage = m.MemberImage! }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EditMovieVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Title = model.Title.Trim();
            if (await IsNameExist(model.Title))
            {
                ModelState.AddModelError("Title", "This Movie is already exists.!");
                return View(model);
            }

            var movie = new Movie
            {
                Title = model.Title,
                IsApproved = true,
                ReleaseYear = model.ReleaseYear
            };

            movie.Poster = ImageService.NewImageUrl(model.File);

            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.Commit();

            await _unitOfWork.MovieGenres.AddMovieGenresAsync(model.Genres, movie.MovieId);
            await _unitOfWork.MovieMembers.AddMovieMembersAsync(model.Members, movie.MovieId);

            await _unitOfWork.Commit();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _unitOfWork.Movies.FindAsync(m => m.MovieId == id, new[] { "Genres.Genre", "Members.Member" });
            if (movie is null) return RedirectToAction(nameof(Index));

            EditMovieVM model = await FillEditModel(movie);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMovieVM model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Title = model.Title.Trim();
            if (await IsNameExist(model.Title, model.MovieId))
            {
                ModelState.AddModelError("Title", "This Movie is already exists.!");
                return View(model);
            }
            if (!model.Genres!.Any(g => g.IsSelected) || !model.Members!.Any(g => g.IsSelected))
            {
                ViewBag.errorMessage = "Please select at least one Genre and one Member.!";
                return View(model);
            }

            model.Poster = ImageService.NewImageUrl(model.File, model.Poster);

            var movie = await _unitOfWork.Movies.FindAsync(m => m.MovieId == model.MovieId, new[] { "Genres", "Members" });
            if (movie is null) return RedirectToAction(nameof(Index));

            await _unitOfWork.MovieGenres.EditMovieGenresAsync(movie.Genres, model.Genres, movie.MovieId);
            await _unitOfWork.MovieMembers.EditMovieMembersAsync(movie.Members, model.Members, movie.MovieId);

            movie.Title = model.Title;
            movie.Poster = model.Poster;
            movie.ReleaseYear = model.ReleaseYear;

            _unitOfWork.Movies.Update(movie);

            if (await _unitOfWork.Commit())
                return RedirectToAction(nameof(Index));


            ViewBag.errorMessage = "Please try again.!";
            return View(model);
        }

        private async Task<EditMovieVM> FillEditModel(Movie movie)
        {
            var allGenres = await _unitOfWork.Genres.GetAllAsync(new[] { "Movies" });
            var allMembers = await _unitOfWork.Members.GetAllAsync(new[] { "Movies" });

            EditMovieVM model = new()
            {
                MovieId = movie!.MovieId,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Poster = movie.Poster,
                Genres = allGenres!.Select(genre => new CheckboxVM
                {
                    ItemId = genre.GenreId,
                    ItemName = genre.GenreName is null ? "" : genre.GenreName,
                    IsSelected = genre.Movies is null ? false : genre.Movies.Any(g => g.MovieId == movie.MovieId) ? true : false
                }).ToList(),
                Members = allMembers?.Select(member => new CheckboxVM
                {
                    ItemId = member.MemberId,
                    ItemImage = member.MemberImage is null ? "DefaultImage.jpg" : member.MemberImage,
                    ItemName = member.MemberName is null ? "" : member.MemberName,
                    IsSelected = member.Movies is null ? false : member.Movies.Any(m => m.MovieId == movie.MovieId) ? true : false
                }).ToList()
            };

            return model;
        }

        private async Task<bool> IsNameExist(string name, int? id = -1)
            => (await _unitOfWork.Movies.FindAsync(m => m.Title.ToLower() == name.ToLower() && m.MovieId != id) is not null);
    }
}