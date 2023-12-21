using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LAB_PROJECT.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public Product()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

    }

    public class Book : Product
    {
        public string Author { get; set; }
    }
}
