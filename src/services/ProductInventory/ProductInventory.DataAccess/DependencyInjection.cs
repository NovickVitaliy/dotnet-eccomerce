using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.DataAccess.Repositories.Implementations;

namespace ProductInventory.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbConnectionAccessor>(_ => new DbConnectionAccessor(configuration.GetConnectionString(DbConnectionAccessor.ConnectionStringPosition)
                                                                               ?? throw new ArgumentNullException(nameof(DbConnectionAccessor.ConnectionStringPosition))));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTagRepository, ProductTagRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        return services;
    }
}