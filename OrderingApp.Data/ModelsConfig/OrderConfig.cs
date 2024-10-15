using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;

namespace OrderingApp.Data.ModelsConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.Notifications)
               .HasDefaultValue(true);

            builder.Property(x => x.CreationDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.OrderSignups)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
