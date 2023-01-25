namespace MoviesMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountRepository.RegisterAsync(model);

            if (result.StatusCode == 1) return RedirectToAction(nameof(Login));

            ViewBag.RegisterError = result.Message;
            return View(model);
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountRepository.LoginAsync(model);

            if (result.StatusCode == 1) return RedirectToAction("Index", "Home");

            ViewBag.LoginError = result.Message;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}