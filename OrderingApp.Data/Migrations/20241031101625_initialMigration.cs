using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    IsExtras = table.Column<bool>(type: "bit", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinValue = table.Column<float>(type: "real", nullable: false),
                    DeliveryCost = table.Column<float>(type: "real", nullable: false),
                    FreeDeliveryFrom = table.Column<float>(type: "real", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<decimal>(type: "decimal(26,0)", precision: 26, scale: 0, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSignups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSignups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSignups_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SignupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_OrderSignups_SignupId",
                        column: x => x.SignupId,
                        principalTable: "OrderSignups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderSignups_SignupId",
                        column: x => x.SignupId,
                        principalTable: "OrderSignups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name", "RestaurantType" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Panda Express", 0 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Joe's Pizza", 2 },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Burger Shack", 3 },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Street Tacos", 1 },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "The Cozy Cafe", 6 }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Blocked", "Calories", "Description", "IsExtras", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), false, 324, "Spicy stir-fried chicken with peanuts and vegetables.", true, "Kung Pao Chicken", 10.99f, new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), false, 324, "Crispy pork in sweet and sour sauce with bell peppers and pineapple.", true, "Sweet and Sour Pork", 9.99f, new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), false, 523, "Stir-fried noodles with vegetables and your choice of meat.", false, "Chow Mein", 8.99f, new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), false, 442, "Crispy rolls filled with vegetables and served with dipping sauce.", false, "Spring Rolls", 5.99f, new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), false, 543, "Fried rice with eggs, peas, and carrots.", true, "Egg Fried Rice", 4.99f, new Guid("00000001-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), false, 552, "Classic pizza with tomato, mozzarella, and fresh basil.", false, "Margherita Pizza", 12.99f, new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), false, 324, "Layers of pasta with meat, cheese, and marinara sauce.", true, "Lasagna", 14.99f, new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), false, 324, "Penne pasta in a spicy tomato sauce with garlic.", false, "Penne Arrabbiata", 11.99f, new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), false, 324, "Coffee-flavored dessert with mascarpone cheese.", true, "Tiramisu", 6.99f, new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), false, 532, "Toasted bread topped with garlic butter and parsley.", false, "Garlic Bread", 3.99f, new Guid("00000002-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), false, 632, "Juicy beef patty with cheese, lettuce, and tomato.", true, "Cheeseburger", 9.99f, new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), false, 783, "Crispy golden fries served with ketchup.", true, "Fries", 2.99f, new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), false, 643, "Breaded chicken pieces served with dipping sauce.", false, "Chicken Nuggets", 6.99f, new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), false, 532, "Creamy milkshake available in chocolate or vanilla.", true, "Milkshake", 4.99f, new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), false, 644, "Crispy onion rings with a side of ranch dressing.", false, "Onion Rings", 3.99f, new Guid("00000003-0000-0000-0000-000000000000") },
                    { new Guid("00000010-0000-0000-0000-000000000000"), false, 200, "Pork tacos with pineapple, onion, and cilantro.", true, "Taco al Pastor", 3.5f, new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), false, 642, "Burrito filled with beans, rice, and fresh vegetables.", false, "Vegetarian Burrito", 8.99f, new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), false, 644, "Grilled tortilla filled with cheese and served with salsa.", false, "Quesadilla", 6.99f, new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), false, 532, "Grilled corn topped with cheese and chili powder.", false, "Elote (Corn on the Cob)", 4.5f, new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), false, 783, "Tortilla chips topped with cheese, jalapenos, and guacamole.", true, "Nachos", 5.99f, new Guid("00000004-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), false, 783, "Toasted bread topped with mashed avocado and poached egg.", false, "Avocado Toast", 7.99f, new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), false, 644, "Rich espresso topped with steamed milk and foam.", true, "Cappuccino", 3.99f, new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), false, 532, "Crisp romaine lettuce with Caesar dressing and croutons.", true, "Caesar Salad", 6.99f, new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), false, 642, "Freshly baked muffin loaded with blueberries.", true, "Blueberry Muffin", 2.99f, new Guid("00000005-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), false, 643, "Chicken salad served on toasted bread with lettuce.", false, "Chicken Salad Sandwich", 8.99f, new Guid("00000005-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SignupId",
                table: "Comments",
                column: "SignupId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DishId",
                table: "OrderItems",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SignupId",
                table: "OrderItems",
                column: "SignupId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSignups_OrderId",
                table: "OrderSignups",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "OrderSignups");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
