namespace MoviesMVC.Repositories.Implementations
{
    public class MovieGenreRepository : GenericRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(AppDbContext context) : base(context) { }

        // This method uses Declarative -Programming- Code (with LINQ declarative, functional programming.)
        public async Task EditMovieGenresAsync(IEnumerable<MovieGenre>? oldMovieGenres, IEnumerable<CheckboxVM>? allMovieGenres, int movieId)
        {
            if (oldMovieGenres is not null) _context.MovieGenres!.RemoveRange(oldMovieGenres);
            await _context.MovieGenres!.AddRangeAsync(allMovieGenres!.Where(x => x.IsSelected)
                .Select(x => new MovieGenre { GenreId = x.ItemId, MovieId = movieId }));
        }

        public async Task AddMovieGenresStringAsync(string genres, int movieId)
        {
            foreach (var genreName in genres.Split(',').Distinct())
            {
                var genre = await _context.Genres!.FirstOrDefaultAsync(g => g.GenreName.ToLower() == genreName.Trim().ToLower());
                if (genre is not null)
                {
                    await _context.MovieGenres!.AddAsync(new MovieGenre { MovieId = movieId, GenreId = genre.GenreId });
                }
            }
        }
    }
}