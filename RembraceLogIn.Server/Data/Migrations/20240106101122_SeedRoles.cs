using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RembraceLogIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29910cff-dfec-4138-83a0-278ecbd143fb", "2ebc45e5-99b9-40a6-ae36-fdac0d4bd0b6", "Admin", "ADMIN" },
                    { "7c09dd89-0b27-4bee-aae4-08c5e30bc4ac", "b7abfc4a-b3e3-4045-b0e2-e90593528cdd", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29910cff-dfec-4138-83a0-278ecbd143fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c09dd89-0b27-4bee-aae4-08c5e30bc4ac");
        }
    }
}
