using System.Security.Claims;

namespace MoviesMVC.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<AppUser> userManager, IMapper mapper,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<Status> LoginAsync(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !_userManager.CheckPasswordAsync(user, model.Password).Result)
                return StatusResult(0, "Invalid email or password.");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                foreach (var userRole in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                return StatusResult(1, "Logged in successfully");
            }
            //else if (result.IsLockedOut) return StatusResult(0, "User is locked out");
            else return StatusResult(0, "Error in login");

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(RegisterVM model)
        {
            Status status = new();

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return StatusResult(0, "User already exist");

            var user = _mapper.Map<AppUser>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return StatusResult(0, "User creation failed");

            await _userManager.AddToRoleAsync(user, "User");

            return StatusResult(1, "You have registered successfully");
        }

        private Status StatusResult(int statusCode, string message)
        {
            var status = new Status
            {
                StatusCode = statusCode,
                Message = message
            };
            return status;
        }
    }
}