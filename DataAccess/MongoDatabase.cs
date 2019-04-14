using System;
using System.Collections.Generic;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess
{
    public class MongoDatabase
    {
        private static readonly IMongoClient client = new MongoClient();
        private static readonly IMongoDatabase db = client.GetDatabase("projekt3");

        #region FindSortLimit

        //public void AddComment(int id, string comment)
        //{
        //    var newComment = new Comment
        //    {
        //        Text = comment,
        //        Timestamp = DateTime.Now
        //    };

        //    var collection = db.GetCollection<News>("news");
        //    var filter = Builders<News>.Filter.Eq("_id", id);
        //    var update = Builders<News>.Update.Push("comments", newComment);

        //    collection.UpdateOne(filter, update);
        //}

        //public List<News> GetNews(int number)
        //{
        //    var collection = db.GetCollection<News>("news");
        //    var filter = new BsonDocument();
        //    var sort = Builders<News>.Sort.Descending("_id");

        //    var result = collection.Find(filter)
        //                            .Sort(sort)
        //                            .Limit(number)
        //                            .ToList();

        //    return result;
        //}

        //public void AddNews(News model)
        //{
        //    var collection = db.GetCollection<News>("news");
        //    model.Id = (int)collection.Count(new BsonDocument());

        //    collection.InsertOne(model);
        //}

        #endregion

        public void AddComment(int id, string comment)
        {
            var newComment = new Comment
            {
                Text = comment,
                Timestamp = DateTime.Now
            };

            var collection = db.GetCollection<News>("news");
            var filter = Builders<News>.Filter.Eq("_id", id);
            var update = Builders<News>.Update.Push("comments", newComment);

            collection.UpdateOne(filter, update);
        }

        public List<News> GetNews(int number)
        {
            var result = new List<News>();
            var collection = db.GetCollection<News>("news");
            var count = collection.Count(new BsonDocument());
            var filter = count > 0 ? 
                Builders<News>.Filter.Eq("_id", count-1) : 
                new BsonDocument();

            var nextNews = collection.Find(filter).SingleOrDefault();
            if (nextNews != null)
            {
                result.Add(nextNews);

                for (var i = 0; i < (number - 1); i++)
                {
                    if (nextNews.NextNews == null)
                    {
                        break;
                    }
                    var newFilter = Builders<News>.Filter.Eq("_id", (int)nextNews.NextNews);
                    nextNews = collection.Find(newFilter).SingleOrDefault();

                    result.Add(nextNews);
                }
            }


            return result;
        }

        public void AddNews(News model)
        {
            var collection = db.GetCollection<News>("news");
            model.Id = (int)collection.Count(new BsonDocument());

            if ((model.Id - 1) > -1)
            {
                model.NextNews = model.Id - 1;
            }

            collection.InsertOne(model);
        }
    }
}
