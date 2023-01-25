namespace MoviesMVC.Models
{
    public class MovieMember
    {
        public Member? Member { get; set; }
        public int MemberId { get; set; }

        public Movie? Movie { get; set; }
        public int MovieId { get; set; }
    }
}