using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopVerseECommercePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedproductisActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fbcc0-977e-745c-a7b1-1c55de47b9b5"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fe595-6900-7b1a-b037-d07fe9458e9e"), "", new DateTimeOffset(new DateTime(2026, 8, 9, 13, 43, 22, 46, DateTimeKind.Unspecified).AddTicks(1512), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fe595-6900-7b1a-b037-d07fe9458e9e"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fbcc0-977e-745c-a7b1-1c55de47b9b5"), "", new DateTimeOffset(new DateTime(2026, 8, 1, 15, 26, 6, 139, DateTimeKind.Unspecified).AddTicks(7088), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }
    }
}
