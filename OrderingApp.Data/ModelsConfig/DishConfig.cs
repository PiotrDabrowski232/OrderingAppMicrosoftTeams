using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;
using OrderingApp.Shared.Extensions;

namespace OrderingApp.Data.ModelsConfig
{
    public class DishConfig : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.OrderItems)
               .WithOne(x => x.Dish)
               .HasForeignKey(x => x.DishId)
               .OnDelete(DeleteBehavior.NoAction);

            var dishes = new Dish[]
            {
                CreateDish(1.ToGuid(), "Kung Pao Chicken", "Spicy stir-fried chicken with peanuts and vegetables.", 10.99f, 1.ToGuid(), true, 324),
                CreateDish(2.ToGuid(), "Sweet and Sour Pork", "Crispy pork in sweet and sour sauce with bell peppers and pineapple.", 9.99f, 1.ToGuid(), true, 324),
                CreateDish(3.ToGuid(), "Chow Mein", "Stir-fried noodles with vegetables and your choice of meat.", 8.99f, 1.ToGuid(), false, 523),
                CreateDish(4.ToGuid(), "Spring Rolls", "Crispy rolls filled with vegetables and served with dipping sauce.", 5.99f, 1.ToGuid(), false, 442),
                CreateDish(5.ToGuid(), "Egg Fried Rice", "Fried rice with eggs, peas, and carrots.", 4.99f, 1.ToGuid(), true, 543),

                CreateDish(6.ToGuid(), "Margherita Pizza", "Classic pizza with tomato, mozzarella, and fresh basil.", 12.99f, 2.ToGuid(),false, 552),
                CreateDish(7.ToGuid(), "Lasagna", "Layers of pasta with meat, cheese, and marinara sauce.", 14.99f, 2.ToGuid(), true, 324),
                CreateDish(8.ToGuid(), "Penne Arrabbiata", "Penne pasta in a spicy tomato sauce with garlic.", 11.99f, 2.ToGuid(), false, 324),
                CreateDish(9.ToGuid(), "Tiramisu", "Coffee-flavored dessert with mascarpone cheese.", 6.99f, 2.ToGuid(), true, 324),
                CreateDish(10.ToGuid(), "Garlic Bread", "Toasted bread topped with garlic butter and parsley.", 3.99f, 2.ToGuid(), false, 532),

                CreateDish(11.ToGuid(), "Cheeseburger", "Juicy beef patty with cheese, lettuce, and tomato.", 9.99f, 3.ToGuid(), true, 632),
                CreateDish(12.ToGuid(), "Fries", "Crispy golden fries served with ketchup.", 2.99f, 3.ToGuid(), true, 783),
                CreateDish(13.ToGuid(), "Chicken Nuggets", "Breaded chicken pieces served with dipping sauce.", 6.99f, 3.ToGuid(), false, 643),
                CreateDish(14.ToGuid(), "Milkshake", "Creamy milkshake available in chocolate or vanilla.", 4.99f, 3.ToGuid(), true, 532),
                CreateDish(15.ToGuid(), "Onion Rings", "Crispy onion rings with a side of ranch dressing.", 3.99f, 3.ToGuid(), false, 644),

                CreateDish(16.ToGuid(), "Taco al Pastor", "Pork tacos with pineapple, onion, and cilantro.", 3.50f, 4.ToGuid(), true, 200),
                CreateDish(17.ToGuid(), "Vegetarian Burrito", "Burrito filled with beans, rice, and fresh vegetables.", 8.99f, 4.ToGuid(), false, 642),
                CreateDish(18.ToGuid(), "Quesadilla", "Grilled tortilla filled with cheese and served with salsa.", 6.99f, 4.ToGuid(), false, 644),
                CreateDish(19.ToGuid(), "Elote (Corn on the Cob)", "Grilled corn topped with cheese and chili powder.", 4.50f, 4.ToGuid(), false, 532),
                CreateDish(20.ToGuid(), "Nachos", "Tortilla chips topped with cheese, jalapenos, and guacamole.", 5.99f, 4.ToGuid(), true, 783),

                CreateDish(21.ToGuid(), "Avocado Toast", "Toasted bread topped with mashed avocado and poached egg.", 7.99f, 5.ToGuid(), false, 783),
                CreateDish(22.ToGuid(), "Cappuccino", "Rich espresso topped with steamed milk and foam.", 3.99f, 5.ToGuid(), true, 644),
                CreateDish(23.ToGuid(), "Caesar Salad", "Crisp romaine lettuce with Caesar dressing and croutons.", 6.99f, 5.ToGuid(), true, 532),
                CreateDish(24.ToGuid(), "Blueberry Muffin", "Freshly baked muffin loaded with blueberries.", 2.99f, 5.ToGuid(), true, 642),
                CreateDish(25.ToGuid(), "Chicken Salad Sandwich", "Chicken salad served on toasted bread with lettuce.", 8.99f, 5.ToGuid(), false, 643),
            };

            builder.HasData(dishes);
        }

        private static Dish CreateDish(Guid id, string name, string description, float price, Guid restaurantId, bool isExtras, int calories)
        {
            return new Dish
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                RestaurantId = restaurantId,
                IsExtras = isExtras,
                Calories = calories
            };
        }
    }
}

