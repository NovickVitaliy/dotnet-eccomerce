using BasketService.Application;
using BasketService.Infrastructure;
using BasketService.Infrastructure.DbSetup;
using BasketService.Infrastructure.Persistence;
using BasketService.Services;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationLayer()
    .ConfigureInfrastructureLayer();
builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbContext>(_ =>
{
    var connectionString = builder.Configuration["MongoDBSettings:ConnectionString"] ?? throw new ArgumentNullException();
    var databaseName = builder.Configuration["MongoDBSettings:DatabaseName"] ?? throw new ArgumentNullException();

    return new MongoDbContext(connectionString, databaseName);
});

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    await app.SetupDatabase(app.Configuration);
}
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();

app.MapControllers();

app.Run();