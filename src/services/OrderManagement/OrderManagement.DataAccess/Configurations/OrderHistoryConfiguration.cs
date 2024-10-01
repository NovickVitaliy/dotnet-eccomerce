using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Configurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> b)
    {
        b.HasKey(x => x.Id);

        b.HasOne(x => x.Order)
            .WithOne(x => x.OrderHistory)
            .HasForeignKey<OrderHistory>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Property(x => x.StatusChangedDate)
            .IsRequired();

        b.Property(x => x.NewStatus)
            .IsRequired();
    }
}