using ProductInventory.Business;
using ProductInventory.DataAccess;
using ProductInventory.DataAccess.Persistance.Initialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureDataAccess(builder.Configuration)
    .ConfigureBusinessLayer();
builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabase();
}


app.MapControllers();
app.Run();