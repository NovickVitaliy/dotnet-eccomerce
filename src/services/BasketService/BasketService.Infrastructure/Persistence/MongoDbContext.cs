using MongoDB.Driver;

namespace BasketService.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;
    
    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _mongoDatabase = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task CreateCollectionsIfNotExistAsync(string[] collectionNames)
    {
        var existingCollection = await (await _mongoDatabase.ListCollectionNamesAsync()).ToListAsync();
        foreach (string collectionName in collectionNames)
        {
            if (!existingCollection.Contains(collectionName))
            {
                await _mongoDatabase.CreateCollectionAsync(collectionName);
            }
        }
    }
}