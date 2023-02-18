namespace MoviesMVC.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IMemberRepository Members { get; }
        IMovieGenreRepository MovieGenres { get; }
        IMovieMemberRepository MovieMembers { get; }
        IOrderRepository Orders { get; }
        IMessageRepository Messages { get; }
        bool HasChanges();
        Task<bool> Commit();
    }
}