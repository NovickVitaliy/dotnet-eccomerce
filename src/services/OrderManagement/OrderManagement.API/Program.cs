using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString(AppDbContext.ConnectionStringPosition));
});

var app = builder.Build();


app.Run();