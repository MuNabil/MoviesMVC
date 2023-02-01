namespace MoviesMVC.ViewModels
{
    public class MovieVM
    {
        public int? MovieId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ReleaseYear { get; set; }

        public IFormFile? File { get; set; }
        public string? Poster { get; set; }

        [Required]
        public string Genres { get; set; } = string.Empty;

        [Required]
        public string Members { get; set; } = string.Empty;
    }
}