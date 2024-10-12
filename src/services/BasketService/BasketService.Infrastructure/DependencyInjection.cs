using BasketService.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IBasketService, Services.BasketService>();
        
        return services;
    }
}