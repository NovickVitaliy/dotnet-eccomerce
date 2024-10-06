using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Configurations;

public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
{
    public void Configure(EntityTypeBuilder<CustomerOrder> builder)
    {
        builder.HasKey(x => new { x.CustomerId, x.OrderId });

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerOrders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.CustomerOrders)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}