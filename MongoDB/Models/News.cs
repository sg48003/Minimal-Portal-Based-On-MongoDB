using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models
{
    public class News
    {
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("headline")]
        public string Headline { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("picture")]
        public byte[] Picture { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
