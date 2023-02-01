namespace MoviesMVC.ViewModels
{
    public class EditMovieVM
    {
        public int? MovieId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ReleaseYear { get; set; }

        public IFormFile? File { get; set; }
        public string? Poster { get; set; }

        [Required]
        public List<CheckboxVM>? Genres { get; set; }

        [Required]
        public List<CheckboxVM>? Members { get; set; }
    }
}