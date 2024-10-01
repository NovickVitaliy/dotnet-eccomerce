using ProductInventory.DataAccess.Initialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabase();
}

app.Run();