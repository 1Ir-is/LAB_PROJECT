using LAB_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMongoDatabase _mongoDatabase;

    public ProductsController(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var products = _mongoDatabase.GetCollection<Product>("Products");
        return products.Find(_ => true).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(string id)
    {
        var product = GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public ActionResult<Product> PostProduct(Book product)
    {
        var products = _mongoDatabase.GetCollection<Product>("Products");
        products.InsertOne(product);

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult PutProduct(string id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var products = _mongoDatabase.GetCollection<Product>("Products");
        var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id));
        var update = Builders<Product>.Update
            .Set("Name", product.Name)
            .Set("Price", product.Price);

        products.UpdateOne(filter, update);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(string id)
    {
        var products = _mongoDatabase.GetCollection<Product>("Products");
        products.DeleteOne(Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id)));

        return NoContent();
    }

    private Product GetProductById(string id)
    {
        var products = _mongoDatabase.GetCollection<Product>("Products");
        return products.Find(Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();
    }
}
