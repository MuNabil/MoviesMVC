namespace MoviesMVC.ViewModels
{
    public class MemberVM
    {
        public int? MemberId { get; set; }

        [Required]
        public string MemberName { get; set; } = string.Empty;

        [Required]
        public string DateOfBirth { get; set; } = string.Empty;

        public string? MemberImage { get; set; }
        public IFormFile? File { get; set; }
        public string? MemberAbout { get; set; }

        [Required]
        public string? MemberRole { get; set; }
    }
}