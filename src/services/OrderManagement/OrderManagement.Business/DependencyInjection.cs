using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Business.Services.Contracts;
using OrderManagement.Business.Services.Implementations;

namespace OrderManagement.Business;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}