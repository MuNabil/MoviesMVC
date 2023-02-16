namespace MoviesMVC.Models
{
    public class Order
    {
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }
        public bool IsApproved { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? ApprovedDate { get; set; }
    }
}