using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistence;

namespace OrderManagement.API;

public static class DbSetup
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();
    }
}