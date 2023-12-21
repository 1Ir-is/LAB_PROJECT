using LAB_PROJECT.Models;
using MongoDB.Driver;
using System.Collections.Generic;

public class ProductService
{
    private readonly MongoDbContext _mongoDbContext;

    public ProductService(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public List<Product> GetAllProducts()
    {
        return _mongoDbContext.Products.Find(_ => true).ToList();
    }

    // Add other MongoDB operations as needed...
}
