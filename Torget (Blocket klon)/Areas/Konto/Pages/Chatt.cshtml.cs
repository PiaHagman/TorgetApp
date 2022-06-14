using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private ChatService _chatService ;
        private UserManager<TorgetUser> _userManager;
        public string CurrentUserId { get { return _userManager.GetUserId(HttpContext.User); } }
        public List<AdMessages>? UsersAdMessages { get; set; }
        public int AdId { get; set; }
        public string SellerId { get; set; }

        public IndexModel(ChatService chatService, UserManager<TorgetUser> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
        }

        public void OnGet(int adId, string sellerId)
        {
            bool sellerIsStarting = CurrentUserId == sellerId;
            bool chatExists = _chatService.DoesChatExist(new MessagesFilter { AdId = adId, BuyerUserId = CurrentUserId, SellerUserId = sellerId});

            if (adId != 0 && sellerId != null && !chatExists && !sellerIsStarting)
            {
                AdId = adId;
                SellerId = sellerId;
            }

            MessagesFilter target = new MessagesFilter { BuyerUserId = CurrentUserId, SellerUserId = CurrentUserId };
            List<AdMessages> result = _chatService.GetMessages(target);
            UsersAdMessages = result;
        }

    }

    public class ChattHub : Hub
    {
        private ChatService _chatService;

        public string? currentUserId { get { return Context.UserIdentifier; } }

        public ChattHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string adId, string content)
        {
            if (currentUserId == null) throw new Exception("There is no logged in user D:");
            MessagesFilter target = new MessagesFilter { AdId = Int16.Parse(adId), BuyerUserId = currentUserId, SellerUserId = currentUserId };

            // Saves the message to document
            Message message = new Message { Content = content, UserId = currentUserId };
            await _chatService.SaveMessageAsync(target, message);

            // Sends the message to receiver
            string receiverId = _chatService.GetMessageReceiver(target, currentUserId);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", content, adId);
        }

        public AdMessages GetMessage(string chatId)
        {
            return _chatService.GetMessage(chatId);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Status", "Connected successfully.");
        }
    }
}
