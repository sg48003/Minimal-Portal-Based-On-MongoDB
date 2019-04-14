using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models
{
    public class Comment
    {
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

    }
}
