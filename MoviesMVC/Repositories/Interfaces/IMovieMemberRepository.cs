namespace MoviesMVC.Repositories.Interfaces
{
    public interface IMovieMemberRepository : IGenericRepository<MovieMember>
    {
        Task EditMovieMembersAsync(IEnumerable<MovieMember>? oldMovieMembers, IEnumerable<CheckboxVM>? allMovieMembers, int movieId);
        Task AddToMovieAsync(int movieId, int memberId);
        Task AddMovieMembersAsync(IEnumerable<CheckboxVM>? allMovieMembers, int movieId);
    }
}