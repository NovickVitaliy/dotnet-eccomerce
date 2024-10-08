using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.Business.Services.Implementations;

namespace ProductInventory.Business;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductDetailService, ProductDetailService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductTagService, ProductTagService>();
        services.AddScoped<ISupplierService, SupplierService>();
        return services;
    }
}