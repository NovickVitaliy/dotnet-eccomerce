using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.DataAccess.Repositories.Implementations;

namespace OrderManagement.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt 
            => opt.UseSqlServer(configuration.GetConnectionString(AppDbContext.ConnectionStringPosition)));
        
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();

        return services;
    }
}