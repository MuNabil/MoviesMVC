using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesMVC.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string SenderEmail { get; set; } = string.Empty;
        public string? SenderId { get; set; }
        public AppUser? Sender { get; set; }

        public string RecipientEmail { get; set; } = string.Empty;
        public string? RecipientId { get; set; } = string.Empty;
        public AppUser? Recipient { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime SendAt { get; set; } = DateTime.Now;
        public DateTime? ReadAt { get; set; }
    }
}