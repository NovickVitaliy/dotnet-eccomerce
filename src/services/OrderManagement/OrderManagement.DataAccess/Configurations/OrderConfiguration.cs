using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .IsRequired();

        builder.Property(x => x.OrderStatus)
            .IsRequired();

        builder.Property(x => x.OrderDate)
            .IsRequired();

        builder.Property(x => x.ShippingAddress)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Shipment)
            .WithOne(x => x.Order)
            .HasForeignKey<Shipment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.OrderHistory)
            .WithOne(x => x.Order)
            .HasForeignKey<OrderHistory>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}