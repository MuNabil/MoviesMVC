namespace MoviesMVC.ViewModels
{
    public class MessagesThreadVM
    {
        public IEnumerable<Message>? Messages { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
    }
}