using BasketService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Infrastructure.DbSetup;

public static class MongoDbSetup
{
    public static async Task SetupDatabase(this WebApplication app, IConfiguration configuration)
    {
        var mongoDbContext = app.Services.GetRequiredService<MongoDbContext>();

        await mongoDbContext.CreateCollectionsIfNotExistAsync(
            [configuration["MongoDBSettings:BasketsCollectionName"] ?? throw new ArgumentNullException()]);
    }
}