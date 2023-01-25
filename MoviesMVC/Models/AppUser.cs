namespace MoviesMVC.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}