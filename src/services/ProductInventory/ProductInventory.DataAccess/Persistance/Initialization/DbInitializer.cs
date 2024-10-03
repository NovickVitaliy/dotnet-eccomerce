using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace ProductInventory.DataAccess.Persistance.Initialization;

public static class DbInitializer
{
    public static Task MigrateDatabase(this WebApplication app)
    {
        var pgRootConnection = app.Configuration.GetConnectionString("SqlServer");
        
        EnsureDatabase.For.SqlDatabase(pgRootConnection);

        var upgrader = DeployChanges.To.SqlDatabase(pgRootConnection)
            .WithScriptsEmbeddedInAssembly(typeof(DbInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();
        }

        return Task.CompletedTask;
    }
}