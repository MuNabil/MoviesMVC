namespace MoviesMVC.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        public string GenreName { get; set; } = string.Empty;

        public bool IsApproved { get; set; }

        public ICollection<MovieGenre>? Movies { get; set; }
    }
}