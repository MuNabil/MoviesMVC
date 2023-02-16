namespace MoviesMVC.Repositories.Implementations
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

    }
}