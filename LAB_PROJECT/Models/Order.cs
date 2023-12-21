using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int UserId { get; set; }

    public List<OrderItem> Items { get; set; }
    public Order()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}

public class OrderItem
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}
