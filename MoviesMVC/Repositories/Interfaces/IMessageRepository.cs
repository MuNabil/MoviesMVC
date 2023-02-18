namespace MoviesMVC.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<List<Message>> GetUnreadedMessagesAsync(string userEmail);
        Task<IEnumerable<Message>> GetMessagesThreadAsync(string currentEmail, string recipientEmail);
    }
}