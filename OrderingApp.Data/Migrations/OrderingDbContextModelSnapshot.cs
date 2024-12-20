﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingApp.Data.DBConfig;

#nullable disable

namespace OrderingApp.Data.Migrations
{
    [DbContext(typeof(OrderingDbContext))]
    partial class OrderingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderingApp.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SignupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SignupId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExtras")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Dishes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000001-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 324,
                            Description = "Spicy stir-fried chicken with peanuts and vegetables.",
                            IsExtras = true,
                            Name = "Kung Pao Chicken",
                            Price = 10.99f,
                            RestaurantId = new Guid("00000001-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000002-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 324,
                            Description = "Crispy pork in sweet and sour sauce with bell peppers and pineapple.",
                            IsExtras = true,
                            Name = "Sweet and Sour Pork",
                            Price = 9.99f,
                            RestaurantId = new Guid("00000001-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000003-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 523,
                            Description = "Stir-fried noodles with vegetables and your choice of meat.",
                            IsExtras = false,
                            Name = "Chow Mein",
                            Price = 8.99f,
                            RestaurantId = new Guid("00000001-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000004-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 442,
                            Description = "Crispy rolls filled with vegetables and served with dipping sauce.",
                            IsExtras = false,
                            Name = "Spring Rolls",
                            Price = 5.99f,
                            RestaurantId = new Guid("00000001-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000005-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 543,
                            Description = "Fried rice with eggs, peas, and carrots.",
                            IsExtras = true,
                            Name = "Egg Fried Rice",
                            Price = 4.99f,
                            RestaurantId = new Guid("00000001-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000006-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 552,
                            Description = "Classic pizza with tomato, mozzarella, and fresh basil.",
                            IsExtras = false,
                            Name = "Margherita Pizza",
                            Price = 12.99f,
                            RestaurantId = new Guid("00000002-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000007-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 324,
                            Description = "Layers of pasta with meat, cheese, and marinara sauce.",
                            IsExtras = true,
                            Name = "Lasagna",
                            Price = 14.99f,
                            RestaurantId = new Guid("00000002-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000008-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 324,
                            Description = "Penne pasta in a spicy tomato sauce with garlic.",
                            IsExtras = false,
                            Name = "Penne Arrabbiata",
                            Price = 11.99f,
                            RestaurantId = new Guid("00000002-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000009-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 324,
                            Description = "Coffee-flavored dessert with mascarpone cheese.",
                            IsExtras = true,
                            Name = "Tiramisu",
                            Price = 6.99f,
                            RestaurantId = new Guid("00000002-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000a-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 532,
                            Description = "Toasted bread topped with garlic butter and parsley.",
                            IsExtras = false,
                            Name = "Garlic Bread",
                            Price = 3.99f,
                            RestaurantId = new Guid("00000002-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000b-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 632,
                            Description = "Juicy beef patty with cheese, lettuce, and tomato.",
                            IsExtras = true,
                            Name = "Cheeseburger",
                            Price = 9.99f,
                            RestaurantId = new Guid("00000003-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000c-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 783,
                            Description = "Crispy golden fries served with ketchup.",
                            IsExtras = true,
                            Name = "Fries",
                            Price = 2.99f,
                            RestaurantId = new Guid("00000003-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000d-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 643,
                            Description = "Breaded chicken pieces served with dipping sauce.",
                            IsExtras = false,
                            Name = "Chicken Nuggets",
                            Price = 6.99f,
                            RestaurantId = new Guid("00000003-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000e-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 532,
                            Description = "Creamy milkshake available in chocolate or vanilla.",
                            IsExtras = true,
                            Name = "Milkshake",
                            Price = 4.99f,
                            RestaurantId = new Guid("00000003-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0000000f-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 644,
                            Description = "Crispy onion rings with a side of ranch dressing.",
                            IsExtras = false,
                            Name = "Onion Rings",
                            Price = 3.99f,
                            RestaurantId = new Guid("00000003-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000010-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 200,
                            Description = "Pork tacos with pineapple, onion, and cilantro.",
                            IsExtras = true,
                            Name = "Taco al Pastor",
                            Price = 3.5f,
                            RestaurantId = new Guid("00000004-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000011-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 642,
                            Description = "Burrito filled with beans, rice, and fresh vegetables.",
                            IsExtras = false,
                            Name = "Vegetarian Burrito",
                            Price = 8.99f,
                            RestaurantId = new Guid("00000004-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000012-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 644,
                            Description = "Grilled tortilla filled with cheese and served with salsa.",
                            IsExtras = false,
                            Name = "Quesadilla",
                            Price = 6.99f,
                            RestaurantId = new Guid("00000004-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000013-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 532,
                            Description = "Grilled corn topped with cheese and chili powder.",
                            IsExtras = false,
                            Name = "Elote (Corn on the Cob)",
                            Price = 4.5f,
                            RestaurantId = new Guid("00000004-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000014-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 783,
                            Description = "Tortilla chips topped with cheese, jalapenos, and guacamole.",
                            IsExtras = true,
                            Name = "Nachos",
                            Price = 5.99f,
                            RestaurantId = new Guid("00000004-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000015-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 783,
                            Description = "Toasted bread topped with mashed avocado and poached egg.",
                            IsExtras = false,
                            Name = "Avocado Toast",
                            Price = 7.99f,
                            RestaurantId = new Guid("00000005-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000016-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 644,
                            Description = "Rich espresso topped with steamed milk and foam.",
                            IsExtras = true,
                            Name = "Cappuccino",
                            Price = 3.99f,
                            RestaurantId = new Guid("00000005-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000017-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 532,
                            Description = "Crisp romaine lettuce with Caesar dressing and croutons.",
                            IsExtras = true,
                            Name = "Caesar Salad",
                            Price = 6.99f,
                            RestaurantId = new Guid("00000005-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000018-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 642,
                            Description = "Freshly baked muffin loaded with blueberries.",
                            IsExtras = true,
                            Name = "Blueberry Muffin",
                            Price = 2.99f,
                            RestaurantId = new Guid("00000005-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("00000019-0000-0000-0000-000000000000"),
                            Blocked = false,
                            Calories = 643,
                            Description = "Chicken salad served on toasted bread with lettuce.",
                            IsExtras = false,
                            Name = "Chicken Salad Sandwich",
                            Price = 8.99f,
                            RestaurantId = new Guid("00000005-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BankAccountNumber")
                        .HasPrecision(26)
                        .HasColumnType("decimal(26, 0)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<float>("DeliveryCost")
                        .HasColumnType("real");

                    b.Property<float>("FreeDeliveryFrom")
                        .HasColumnType("real");

                    b.Property<float>("MinValue")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.OrderItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SignupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("SignupId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.OrderSignups", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SignedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserDisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderSignups");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000001-0000-0000-0000-000000000000"),
                            Name = "Panda Express",
                            RestaurantType = 0
                        },
                        new
                        {
                            Id = new Guid("00000002-0000-0000-0000-000000000000"),
                            Name = "Joe's Pizza",
                            RestaurantType = 2
                        },
                        new
                        {
                            Id = new Guid("00000003-0000-0000-0000-000000000000"),
                            Name = "Burger Shack",
                            RestaurantType = 3
                        },
                        new
                        {
                            Id = new Guid("00000004-0000-0000-0000-000000000000"),
                            Name = "Street Tacos",
                            RestaurantType = 1
                        },
                        new
                        {
                            Id = new Guid("00000005-0000-0000-0000-000000000000"),
                            Name = "The Cozy Cafe",
                            RestaurantType = 6
                        });
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Comment", b =>
                {
                    b.HasOne("OrderingApp.Data.Models.OrderSignups", "Signups")
                        .WithMany("Comments")
                        .HasForeignKey("SignupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Signups");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Dish", b =>
                {
                    b.HasOne("OrderingApp.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Dishes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Order", b =>
                {
                    b.HasOne("OrderingApp.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.OrderItems", b =>
                {
                    b.HasOne("OrderingApp.Data.Models.Dish", "Dish")
                        .WithMany("OrderItems")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OrderingApp.Data.Models.OrderSignups", "Signup")
                        .WithMany("OrderItems")
                        .HasForeignKey("SignupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Signup");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.OrderSignups", b =>
                {
                    b.HasOne("OrderingApp.Data.Models.Order", "Order")
                        .WithMany("OrderSignups")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Dish", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Order", b =>
                {
                    b.Navigation("OrderSignups");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.OrderSignups", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OrderingApp.Data.Models.Restaurant", b =>
                {
                    b.Navigation("Dishes");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
