using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class blockedDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000a-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000b-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000c-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000d-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000e-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0000000f-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000000"),
                column: "Blocked",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Dishes");
        }
    }
}
