using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ProductInventory.Business;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        

        return services;
    }
}