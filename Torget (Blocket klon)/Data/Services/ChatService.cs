using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.Services
{
    public class ChatService
    {
        private IMongoCollection<AdMessages> _collection;

        public ChatService()
        {
            var dbClient = new MongoClient("mongodb+srv://admin2:admin2@cluster0.ak8qp.mongodb.net/?retryWrites=true&w=majority");
            _collection = dbClient.GetDatabase("Torget").GetCollection<AdMessages>("Chatt3");
        }

        public async Task SaveMessageAsync(MessagesFilter target, Message message)
        {
            var docExistsAlready = _collection.Find(target.Filter).Any();

            if (docExistsAlready)
            {
                var messagePush = Builders<AdMessages>.Update.Push(x => x.Messages, message);
                var usersActiveChat = await _collection.UpdateOneAsync(target.Filter, messagePush);
            }
            else
            {
                var newAdMessage = new AdMessages
                {
                    AdId = target.AdId,
                    SellerUserId = target.SellerUserId,
                    BuyerUserId = target.BuyerUserId,
                };
                newAdMessage.Messages.Add(message);

                await _collection.InsertOneAsync(newAdMessage);
            }

        }

        public AdMessages GetMessages(MessagesFilter target)
        {
            return _collection.Find(target.Filter).First();
        }

    }

    public class MessagesFilter
    {
        public string AdId { get; set; }
        public string BuyerUserId { get; set; }
        public string SellerUserId { get; set; }

        public FilterDefinition<AdMessages> Filter
        {
            get
            {
                var Filter1 = Builders<AdMessages>.Filter.Eq(x => x.AdId, this.AdId);
                var Filter2 = Builders<AdMessages>.Filter.Eq(x => x.BuyerUserId, this.BuyerUserId);
                var Filter3 = Builders<AdMessages>.Filter.Eq(x => x.SellerUserId, this.SellerUserId);

                return Filter1 & (Filter2 | Filter3);
            }
        }

    }
}
