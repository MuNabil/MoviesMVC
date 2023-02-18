namespace MoviesMVC.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Order>? Orders { get; set; }

        // For Messageing
        public ICollection<Message>? MessagesSent { get; set; }

        public ICollection<Message>? MessagesReceived { get; set; }
    }
}