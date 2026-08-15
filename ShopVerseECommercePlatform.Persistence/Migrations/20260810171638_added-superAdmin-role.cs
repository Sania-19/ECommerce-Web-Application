using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopVerseECommercePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedsuperAdminrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fe63d-0850-7fab-8ce5-44ad574bdc21"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fecad-176f-7af3-bc87-e7cd0d95ad7b"), "", new DateTimeOffset(new DateTime(2026, 8, 10, 22, 46, 34, 541, DateTimeKind.Unspecified).AddTicks(5656), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019fecad-176f-7af3-bc87-e7cd0d95ad7b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmationCode", "CreatedOn", "Email", "Password", "PhoneNo", "Salt", "UserRole", "UserStatus" },
                values: new object[] { new Guid("019fe63d-0850-7fab-8ce5-44ad574bdc21"), "", new DateTimeOffset(new DateTime(2026, 8, 9, 16, 46, 27, 341, DateTimeKind.Unspecified).AddTicks(7067), new TimeSpan(0, 5, 30, 0, 0)), "sania@gmail.com", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi", "9797893466", "$2a$11$YOtZkxWhHmwRR4XiiwA1PO", 1, 1 });
        }
    }
}
