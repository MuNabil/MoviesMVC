namespace MoviesMVC.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ReleaseYear { get; set; }

        public string? Poster { get; set; }

        public bool IsApproved { get; set; }

        public ICollection<MovieGenre>? Genres { get; set; }

        public ICollection<MovieMember>? Members { get; set; }

    }
}