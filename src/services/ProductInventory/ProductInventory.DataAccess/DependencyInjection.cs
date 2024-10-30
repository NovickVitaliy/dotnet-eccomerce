using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.DataAccess.Repositories.Implementations;
using ProductInventory.DataAccess.Repositories.Implementations.Category;
using ProductInventory.DataAccess.Repositories.Implementations.Product;
using ProductInventory.DataAccess.Repositories.Implementations.ProductDetails;
using ProductInventory.DataAccess.Repositories.Implementations.ProductTag;
using ProductInventory.DataAccess.Repositories.Implementations.Supplier;

namespace ProductInventory.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddScoped<DbConnectionAccessor>(_ => new DbConnectionAccessor(configuration.GetConnectionString(DbConnectionAccessor.ConnectionStringPosition)
                                                                               ?? throw new ArgumentNullException(nameof(DbConnectionAccessor.ConnectionStringPosition))));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.Decorate<ICategoryRepository, CachedCategoryRepository>();
        
        services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
        services.Decorate<IProductDetailsRepository, CachedProductDetailsRepository>();
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.Decorate<IProductRepository, CachedProductRepository>();
        
        services.AddScoped<IProductTagRepository, ProductTagRepository>();
        services.AddScoped<IProductTagRepository, CachedProductTagRepository>();
        
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.Decorate<ISupplierRepository, CachedSupplierRepository>();
        
        return services;
    }
}