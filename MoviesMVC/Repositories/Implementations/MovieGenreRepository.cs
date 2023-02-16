namespace MoviesMVC.Repositories.Implementations
{
    public class MovieGenreRepository : GenericRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(AppDbContext context) : base(context) { }

        // This method uses Declarative -Programming- Code (with LINQ declarative, functional programming.)
        public async Task EditMovieGenresAsync(IEnumerable<MovieGenre>? oldMovieGenres, IEnumerable<CheckboxVM>? allMovieGenres, int movieId)
        {
            if (oldMovieGenres is not null) _context.MovieGenres!.RemoveRange(oldMovieGenres);
            await AddMovieGenresAsync(allMovieGenres, movieId);
        }

        public async Task AddMovieGenresAsync(IEnumerable<CheckboxVM>? allMovieGenres, int movieId)
        {
            await _context.MovieGenres!.AddRangeAsync(allMovieGenres!.Where(x => x.IsSelected)
                .Select(x => new MovieGenre { GenreId = x.ItemId, MovieId = movieId }));
        }
    }
}