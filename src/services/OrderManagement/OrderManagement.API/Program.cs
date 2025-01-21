using Microsoft.EntityFrameworkCore;
using OrderManagement.API;
using OrderManagement.Business;
using OrderManagement.DataAccess;
using OrderManagement.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureDataAccess(builder.Configuration)
    .ConfigureBusinessLayer();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString(AppDbContext.ConnectionStringPosition));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}


app.MapControllers();

app.Run();