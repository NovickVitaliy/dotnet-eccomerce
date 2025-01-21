using CommunityService.Application.Data;
using CommunityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.API;

public static class DbSetup
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}