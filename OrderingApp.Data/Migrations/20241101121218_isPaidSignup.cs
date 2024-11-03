using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class isPaidSignup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "OrderSignups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "OrderSignups");
        }
    }
}
