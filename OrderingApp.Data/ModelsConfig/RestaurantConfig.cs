using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Shared.Extensions;

namespace OrderingApp.Data.ModelsConfig
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Dishes)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId);

            var restaurants = new Restaurant[]
            {
                CreateRestaurant(1.ToGuid(), "Panda Express", RestauranType.Chinese),
                CreateRestaurant(2.ToGuid(), "Joe's Pizza", RestauranType.Italian),
                CreateRestaurant(3.ToGuid(), "Burger Shack", RestauranType.FastFood),
                CreateRestaurant(4.ToGuid(), "Street Tacos", RestauranType.Street),
                CreateRestaurant(5.ToGuid(), "The Cozy Cafe", RestauranType.Cafe),
            };

            builder.HasData(restaurants);
        }

        private static Restaurant CreateRestaurant(Guid id, string name, RestauranType restaurantType)
        {
            return new Restaurant
            {
                Id = id,
                Name = name,
                RestaurantType = restaurantType,
            };
        }
    }
}