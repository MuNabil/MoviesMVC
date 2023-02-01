namespace MoviesMVC.Repositories.Interfaces
{
    public interface IMovieGenreRepository : IGenericRepository<MovieGenre>
    {
        Task EditMovieGenresAsync(IEnumerable<MovieGenre>? oldMovieGenres, IEnumerable<CheckboxVM>? allMovieGenres, int movieId);
        Task AddMovieGenresStringAsync(string genres, int movieId);

    }
}