namespace MoviesMVC.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<MessageHub> _messageHub;
        public MessagesController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHub)
        {
            _unitOfWork = unitOfWork;
            _messageHub = messageHub;
        }

        public async Task<ActionResult> UnreadMessages()
        {
            var userEmail = User.GetEmail();

            if (userEmail is null) return RedirectToAction("Index", "Users");

            var messages = await _unitOfWork.Messages.GetUnreadedMessagesAsync(userEmail!);

            return View(messages);
        }

        public async Task<ActionResult> Chat(string userEmail)
        {
            var currentEmail = User.GetEmail();
            if (userEmail == currentEmail)
            {
                TempData["UsersError"] = "You can not chat with yourself.!";
                return RedirectToAction(nameof(Index), "Users");
            }
            MessagesThreadVM model = new()
            {
                SenderEmail = currentEmail!,
                RecipientEmail = userEmail
            };

            model.Messages = await _unitOfWork.Messages.GetMessagesThreadAsync(currentEmail!, userEmail);
            if (_unitOfWork.HasChanges()) await _unitOfWork.Commit();

            return View(model);
        }
    }
}