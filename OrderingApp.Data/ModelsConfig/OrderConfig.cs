using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;
using OrderingApp.Data.Models.Enum;
using System.Reflection.Emit;

namespace OrderingApp.Data.ModelsConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(o => o.Status)
                .HasConversion<int>()      
                .HasDefaultValue(OrderStatus.Active);

            builder.Property(x => x.CreationDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.OrderSignups)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.BankAccountNumber)
                .IsRequired()
                .HasColumnType("decimal(26, 0)") 
                .HasPrecision(26, 0); 
        }
    }
}
