using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;

namespace OrderingApp.Data.ModelsConfig
{
    public class OrderItemsConfig : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
