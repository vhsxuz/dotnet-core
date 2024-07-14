using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "5ea30e49-b60f-4da6-9e06-a85cb2be7a03", "Books" },
                    { "ebc0e8d4-059e-48a3-9507-f43f104032e4", "Toys" },
                    { "ee659432-4186-4d1b-97cb-dbc73bd717a3", "Stationary" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "5ea30e49-b60f-4da6-9e06-a85cb2be7a03");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ebc0e8d4-059e-48a3-9507-f43f104032e4");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ee659432-4186-4d1b-97cb-dbc73bd717a3");
        }
    }
}
