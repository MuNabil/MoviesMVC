namespace MoviesMVC.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IUserRepository Users { get; }
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IMemberRepository Members { get; }
        IMovieGenreRepository MovieGenres { get; }
        IMovieMemberRepository MovieMembers { get; }
        IOrderRepository Orders { get; }
        bool HasChanges();
        Task<bool> Commit();
    }
}