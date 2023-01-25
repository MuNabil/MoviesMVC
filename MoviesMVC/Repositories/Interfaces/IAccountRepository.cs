namespace MoviesMVC.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Status> LoginAsync(LoginVM model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegisterVM model);
    }
}