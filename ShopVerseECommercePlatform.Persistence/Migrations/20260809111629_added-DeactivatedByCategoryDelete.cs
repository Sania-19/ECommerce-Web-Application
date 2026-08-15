using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopVerseECommercePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedDeactivatedByCategoryDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fe595-6900-7b1a-b037-d07fe9458e9e"));

            migrationBuilder.AddColumn<bool>(
                name: "DeactivatedByCategoryDelete",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fe63d-0850-7fab-8ce5-44ad574bdc21"), "", new DateTimeOffset(new DateTime(2026, 8, 9, 16, 46, 27, 341, DateTimeKind.Unspecified).AddTicks(7067), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fe63d-0850-7fab-8ce5-44ad574bdc21"));

            migrationBuilder.DropColumn(
                name: "DeactivatedByCategoryDelete",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fe595-6900-7b1a-b037-d07fe9458e9e"), "", new DateTimeOffset(new DateTime(2026, 8, 9, 13, 43, 22, 46, DateTimeKind.Unspecified).AddTicks(1512), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }
    }
}
