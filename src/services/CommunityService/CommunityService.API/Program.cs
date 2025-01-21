using CommunityService.API;
using CommunityService.Application;
using CommunityService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();
    builder.Services
    .ConfigureApplication(builder.Configuration)
    .ConfigureInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.UseRouting();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
