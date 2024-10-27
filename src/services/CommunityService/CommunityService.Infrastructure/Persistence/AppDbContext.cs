using System.Reflection;
using CommunityService.Application.Data;
using CommunityService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public const string ConnectionStringPosition = "Default";
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Question> Questions => Set<Question>();
    
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}