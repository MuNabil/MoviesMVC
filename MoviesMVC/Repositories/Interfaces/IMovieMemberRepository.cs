namespace MoviesMVC.Repositories.Interfaces
{
    public interface IMovieMemberRepository : IGenericRepository<MovieMember>
    {
        Task EditMovieMembersAsync(IEnumerable<MovieMember>? oldMovieMembers, IEnumerable<CheckboxVM>? allMovieMembers, int movieId);
        Task AddMovieMembersStringAsync(string members, int movieId);

        Task AddToMovieAsync(int movieId, int memberId);
    }
}