namespace MoviesMVC.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required]
        public string MemberName { get; set; } = string.Empty;

        [Required]
        public string DateOfBirth { get; set; } = string.Empty;

        public string? MemberImage { get; set; }
        public string? MemberAbout { get; set; }

        [Required]
        public string MemberRole { get; set; } = string.Empty;

        public bool IsApproved { get; set; }

        public ICollection<MovieMember>? Movies { get; set; }
    }
}