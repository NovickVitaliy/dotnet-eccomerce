using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Business;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}