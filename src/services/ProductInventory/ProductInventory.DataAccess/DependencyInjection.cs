using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductInventory.DataAccess.Persistance;

namespace ProductInventory.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbConnectionAccessor>(_ => new DbConnectionAccessor(configuration.GetConnectionString(DbConnectionAccessor.ConnectionStringPosition)
                                                                               ?? throw new ArgumentNullException(nameof(DbConnectionAccessor.ConnectionStringPosition))));

        return services;
    }
}