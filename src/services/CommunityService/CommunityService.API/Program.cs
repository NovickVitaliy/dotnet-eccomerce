using CommunityService.Application;
using CommunityService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();
    builder.Services
    .ConfigureApplication(builder.Configuration)
    .ConfigureInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
