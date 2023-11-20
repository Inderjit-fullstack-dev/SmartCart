using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCart.Core.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02c7729b-6d5e-4f53-a1a1-a68b79f50727", "2", "CUSTOMER", "CUSTOMER" },
                    { "6e319064-d1a8-414d-ac02-5db6302e76f3", "1", "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02c7729b-6d5e-4f53-a1a1-a68b79f50727");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e319064-d1a8-414d-ac02-5db6302e76f3");
        }
    }
}
