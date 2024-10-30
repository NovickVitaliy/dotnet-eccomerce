using CommunityService.Application.Data;
using CommunityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityService.Infrastructure;

public static class DependencyInjection
{
    private const string CacheConnectionStringKey = "Community.Cache";
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(AppDbContext.ConnectionStringPosition));
        });

        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString(CacheConnectionStringKey);
            options.InstanceName = "Community";
        });
        
        return services;
    }
}