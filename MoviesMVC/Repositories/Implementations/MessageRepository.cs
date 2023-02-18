namespace MoviesMVC.Repositories.Implementations
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context) { }

        public async Task<List<Message>> GetUnreadedMessagesAsync(string userEmail)
        {
            var messages = await _context.Messages!.Include(x => x.Recipient).Include(x => x.Sender)
                .OrderByDescending(m => m.SendAt)
                .Where(m => m.RecipientEmail == userEmail && m.ReadAt == null).ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<Message>> GetMessagesThreadAsync(string currentEmail, string recipientEmail)
        {
            var messages = await _context.Messages!.Include(x => x.Recipient).Include(x => x.Sender)
                                .Where(m => (m.Sender!.Email == currentEmail && m.Recipient!.Email == recipientEmail)
                                    || (m.Recipient!.Email == currentEmail && m.Sender!.Email == recipientEmail))
                                .OrderBy(m => m.SendAt)
                                .ToListAsync();

            var unreadedMessages = messages.Where(m => m.RecipientEmail == currentEmail && m.ReadAt is null).ToList();
            if (unreadedMessages.Any())
            {
                foreach (var message in unreadedMessages)
                {
                    message.ReadAt = DateTime.Now;
                }
            }

            return messages;
        }

    }
}