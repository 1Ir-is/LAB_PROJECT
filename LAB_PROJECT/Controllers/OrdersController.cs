using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMongoDatabase _mongoDatabase;

    public OrdersController(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetOrders()
    {
        var orders = _mongoDatabase.GetCollection<Order>("Orders");
        return orders.Find(_ => true).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(string id)
    {
        var order = GetOrderById(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public ActionResult<Order> PostOrder([FromBody] Order order)
    {
        // Use the UserId from the cookie
        var userId = HttpContext.Request.Cookies["UserId"];

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found in the cookie.");
        }

        order.UserId = int.Parse(userId);

        var orders = _mongoDatabase.GetCollection<Order>("Orders");
        orders.InsertOne(order);

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public IActionResult PutOrder(string id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        var orders = _mongoDatabase.GetCollection<Order>("Orders");
        var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id));
        var update = Builders<Order>.Update
            .Set("UserId", order.UserId)
            .Set("Items", order.Items);

        orders.UpdateOne(filter, update);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(string id)
    {
        var orders = _mongoDatabase.GetCollection<Order>("Orders");
        orders.DeleteOne(Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id)));

        return NoContent();
    }

    private Order GetOrderById(string id)
    {
        var orders = _mongoDatabase.GetCollection<Order>("Orders");
        return orders.Find(Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();
    }
}
