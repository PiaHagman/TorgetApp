namespace Torget__Blocket_klon_.Data.Models
{
    public class AdMessages
    {
        public Guid _id { get; set; } = Guid.NewGuid();
        public string? AdId { get; set; }
        public string? SellerUserId { get; set; }
        public string? BuyerUserId { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }

    public class Message
    {
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public string DateSent { get; set; } = DateTime.Now.ToUniversalTime().ToString();
    }
}
