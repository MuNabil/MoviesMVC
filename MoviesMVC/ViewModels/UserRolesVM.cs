namespace MoviesMVC.ViewModels
{
    public class UserRolesVM
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        [Required]
        public List<RoleVM>? Roles { get; set; }

    }

    public class RoleVM
    {
        public string? RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}