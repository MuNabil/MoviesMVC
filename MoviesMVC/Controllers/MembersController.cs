namespace MoviesMVC.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _unitOfWork.Members.GetAllAsync();
            return View(members);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return GoToCreateView(new MemberVM());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberVM model)
        {
            if (!ModelState.IsValid)
                return GoToCreateView(model);

            model.MemberName = model.MemberName.Trim();
            if (await IsNameExist(model.MemberName))
                return GoToCreateView(model, "This Member is already exists.!");

            // var imageFile = HttpContext.Request.Form.Files.SingleOrDefault(f => f.Name == "ImageFile");

            model.MemberImage = ImageService.NewImageUrl(model.File);

            await _unitOfWork.Members.AddAsync(_mapper.Map<Member>(model));

            if (await _unitOfWork.Commit())
                return RedirectToAction(nameof(Index));

            return GoToCreateView(model, "Please Try again.!");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member is not null && ImageService.DeleteImage(member.MemberImage))
            {
                _unitOfWork.Members.Delete(member);
                await _unitOfWork.Commit();
            }

            else ViewBag.CreateError = "Please Try again.!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var member = await _unitOfWork.Members.FindAsync(m => m.MemberId == id, new[] { "Movies" });
            return View(member);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member is null) return RedirectToAction(nameof(Index));

            return GoToCreateView(_mapper.Map<MemberVM>(member));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberVM model)
        {
            if (!ModelState.IsValid)
                return GoToCreateView(model);

            model.MemberName = model.MemberName.Trim();
            if (await IsNameExist(model.MemberName, model.MemberId))
                return GoToCreateView(model, "This Name is already exists.!");

            model.MemberImage = ImageService.NewImageUrl(model.File, model.MemberImage);

            _unitOfWork.Members.Update(_mapper.Map<Member>(model));

            if (await _unitOfWork.Commit())
                return RedirectToAction(nameof(Index));

            return GoToCreateView(model, "Please Try again.!");
        }

        private IActionResult GoToCreateView(MemberVM model, string? message = null)
        {
            ViewBag.errorMessage = message;
            ViewBag.memberRoles = new List<string> { MemberRoles.Actor, MemberRoles.Director };
            return View(nameof(Create), model);
        }

        private async Task<bool> IsNameExist(string name, int? id = -1)
        {
            return (await _unitOfWork.Members.FindAsync(m => m.MemberName.ToLower() == name.ToLower() && m.MemberId != id) is not null);
        }

    }
}