namespace MoviesMVC.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        private static HashSet<string> UsersOpenChat { get; set; } = new();
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<PresenceHub> _presenceHub;
        public MessageHub(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
             IHubContext<PresenceHub> presenceHub)
        {
            _presenceHub = presenceHub;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public override Task OnConnectedAsync()
        {
            UsersOpenChat.Add(Context.User!.GetEmail()!);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            UsersOpenChat.Remove(Context.User!.GetEmail()!);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string senderEmail, string message, string recipientEmail)
        {
            var sender = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == senderEmail);
            var recipient = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == recipientEmail);
            if (sender is null || recipient is null) return;

            var newMessage = new Message
            {
                Sender = sender,
                SenderEmail = sender.UserName,
                Recipient = recipient,
                RecipientEmail = recipient.UserName,
                Content = message
            };

            await _unitOfWork.Messages.AddAsync(newMessage);
            if (await _unitOfWork.Commit())
            {
                await Clients.Users(new List<string> { sender.Id, recipient.Id }).SendAsync("NewMessage",
                    senderEmail, message, recipientEmail, newMessage.SendAt);

                if (!UsersOpenChat.Contains(recipientEmail))
                    await _presenceHub.Clients.User(recipient.Id).SendAsync("NotifyNewMessage", senderEmail);
            }
        }
    }
}