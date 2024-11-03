using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.Models;

namespace OrderingApp.Data.ModelsConfig
{
    public class OrderSignupsConfig : IEntityTypeConfiguration<OrderSignups>
    {
        public void Configure(EntityTypeBuilder<OrderSignups> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Signup)
                .HasForeignKey(x => x.SignupId);

            builder.Property(x => x.IsPaid)
                .HasDefaultValue(false);
        }
    }
}
