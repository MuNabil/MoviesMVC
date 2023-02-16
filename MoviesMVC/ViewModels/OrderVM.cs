namespace MoviesMVC.ViewModels
{
    public class OrderVM
    {
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string? MoviePoster { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public bool IsApproved { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}