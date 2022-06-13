using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
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
            Console.WriteLine("trying to send a message");

            MessagesFilter target = new MessagesFilter { AdId = Int16.Parse(adId), BuyerUserId = currentUserId, SellerUserId = currentUserId };

            Message message = new Message { Content = content, UserId = currentUserId };
            await _chatService.SaveMessageAsync(target, message);

            string receiverId = _chatService.GetMessageReceiver(target, currentUserId);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", content, adId);
        }

        public List<AdMessages> GetMessages()
        {
            if (currentUserId == null) throw new Exception("There is no logged in user D:");

            MessagesFilter target = new MessagesFilter { BuyerUserId = currentUserId, SellerUserId = currentUserId };
            List<AdMessages> result = _chatService.GetMessages(target);

            return result;
        }

        public AdMessages GetMessage(string id)
        {
            return _chatService.GetMessage(id);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.UserIdentifier + " connected");
            await Clients.All.SendAsync("Status", "Connected successfully.");
        }

        public void Seed()
        {
            AdMessages adMessages = new AdMessages
            {
                AdId = 1,
                SellerUserId = "43eefa21-9b75-4926-9e1f-d9a878aa5f24",
                BuyerUserId = "f20ff2b1-a75d-4ce0-a245-6415284391cf",
            };

            List<Message> messages = new List<Message>
            {
                new()
                {
                    UserId = "f20ff2b1-a75d-4ce0-a245-6415284391cf",
                    Content = "Finns cykeln kvar?"
                },
                new()
                {
                    UserId = "43eefa21-9b75-4926-9e1f-d9a878aa5f24",
                    Content = "Ja den finns kvar, intresserad?"
                },
                new()
                {
                    UserId = "f20ff2b1-a75d-4ce0-a245-6415284391cf",
                    Content = "Nä inte längre :)"
                }
            };

            adMessages.Messages.AddRange(messages);

            _chatService.Seed(adMessages);
        }
    }
}
