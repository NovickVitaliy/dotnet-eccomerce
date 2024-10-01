using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Persistance;

public class AppDbContext : DbContext
{
    public const string ConnectionStringPosition = "SqlServer";
    
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderHistory> OrderHistories => Set<OrderHistory>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}