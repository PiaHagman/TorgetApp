using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.Services
{
    public class ChatService
    {
        private IMongoCollection<AdMessages> _collection;
        private TorgetDbContext _db;

        public ChatService(TorgetDbContext db)
        {
            _db = db;

            var dbClient = new MongoClient("mongodb+srv://admin2:admin2@cluster0.ak8qp.mongodb.net/?retryWrites=true&w=majority");
            _collection = dbClient.GetDatabase("Torget").GetCollection<AdMessages>("Chatt3");
        }

        public async Task SaveMessageAsync(MessagesFilter target, Message message)
        {
            var docExistsAlready = _collection.Find(target.FilterAll).Any();

            if (docExistsAlready)
            {
                var PushMessageIntoList = Builders<AdMessages>.Update.Push(x => x.Messages, message);
                await _collection.UpdateOneAsync(target.FilterAll, PushMessageIntoList);
            }
            else
            {
                var sellerUserId = _db.TorgetAds.Include(x => x.TorgetUser).Where(x => x.Id == target.AdId).First().TorgetUser.Id;
                target.SellerUserId = sellerUserId;

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

        public List<AdMessages> GetMessages(MessagesFilter target)
        {
            return _collection.Find(target.FilterByUser).ToList();
        }

        public bool DoesChatExist(MessagesFilter target)
        {
            return _collection.Find(target.FilterAllStrict).Any();
        }

        public AdMessages GetMessage(string id)
        {
            var idFilter = Builders<AdMessages>.Filter.Eq(x => x._id, new Guid(id));
            return _collection.Find(idFilter).FirstOrDefault();
        }

        internal void Seed(AdMessages adMessages)
        {
            Console.WriteLine("Seeding mongoDb");
            _collection.InsertOne(adMessages);
        }

        public string GetMessageReceiver(MessagesFilter filter, string currentUserId)
        {
            AdMessages target = _collection.Find(filter.FilterAll).First();
            if (target.BuyerUserId == currentUserId) return target.SellerUserId;
            return target.BuyerUserId;

        }
    }

    public class MessagesFilter
    {
        public int AdId { get; set; }
        public string? BuyerUserId { get; set; }
        public string? SellerUserId { get; set; }

        public FilterDefinition<AdMessages> FilterAll
        {
            get
            {
                var Filter1 = Builders<AdMessages>.Filter.Eq(x => x.AdId, this.AdId);
                var Filter2 = Builders<AdMessages>.Filter.Eq(x => x.BuyerUserId, this.BuyerUserId);
                var Filter3 = Builders<AdMessages>.Filter.Eq(x => x.SellerUserId, this.SellerUserId);

                return Filter1 & (Filter2 | Filter3);
            }
        }

        public FilterDefinition<AdMessages> FilterAllStrict
        {
            get
            {
                var Filter1 = Builders<AdMessages>.Filter.Eq(x => x.AdId, this.AdId);
                var Filter2 = Builders<AdMessages>.Filter.Eq(x => x.BuyerUserId, this.BuyerUserId);
                var Filter3 = Builders<AdMessages>.Filter.Eq(x => x.SellerUserId, this.SellerUserId);

                return Filter1 & Filter2 & Filter3;
            }
        }

        public FilterDefinition<AdMessages> FilterByUser
        {
            get
            {
                var Filter1 = Builders<AdMessages>.Filter.Eq(x => x.BuyerUserId, this.BuyerUserId);
                var Filter2 = Builders<AdMessages>.Filter.Eq(x => x.SellerUserId, this.SellerUserId);

                return Filter1 | Filter2;
            }
        }

    }
}
