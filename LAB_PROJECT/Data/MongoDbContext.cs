using LAB_PROJECT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static LAB_PROJECT.Startup;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase("lab_project");
    }

    // Add DbSet properties for each MongoDB collection
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
}
