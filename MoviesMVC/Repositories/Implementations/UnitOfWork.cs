namespace MoviesMVC.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = dbContext;
        }

        public IAccountRepository Accounts => new AccountRepository(_userManager, _mapper, _signInManager, _roleManager);

        public IUserRepository Users => new UserRepository(_context);
        public IMovieRepository Movies => new MovieRepository(_context);

        public IGenreRepository Genres => new GenreRepository(_context);

        public IMemberRepository Members => new MemberRepository(_context);
        public IMovieGenreRepository MovieGenres => new MovieGenreRepository(_context);

        public IMovieMemberRepository MovieMembers => new MovieMemberRepository(_context);
        public IOrderRepository Orders => new OrderRepository(_context);

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}