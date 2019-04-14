using System;
using System.Collections.Generic;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess
{
    public class DataAccess
    {
        private static readonly IMongoClient client = new MongoClient();
        private static readonly IMongoDatabase db = client.GetDatabase("mojportal");

        public void AddComment(int id, string comment)
        {
            var com = new Comment
            {
                Text = comment,
                Timestamp = DateTime.Now
            };

            var collection = db.GetCollection<News>("news");
            var filter = Builders<News>.Filter.Eq("_id", id);
            var update = Builders<News>.Update.Push("comments", com);

            collection.UpdateOne(filter, update);
        }

        public List<News> GetNews(int number)
        {
            var collection = db.GetCollection<News>("news");
            var sort = Builders<News>.Sort.Descending("_id");
            var result = collection.Find(_ => true).Sort(sort).Limit(number).ToList();

            return result;
        }

        public void Add(News news)
        {
            var collection = db.GetCollection<News>("news");
            news.Id = (int)collection.Count(new BsonDocument());

            collection.InsertOne(news);
        }
    }
}
