using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Shipment)
            .HasForeignKey<Shipment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.ShippingDate)
            .IsRequired();

        builder.Property(x => x.EstimatedArrival)
            .IsRequired();

        builder.Property(x => x.TrackingNumber)
            .IsRequired()
            .HasConversion(toDb => toDb.ToString(),
                fromDb => Guid.Parse(fromDb));
    }
}