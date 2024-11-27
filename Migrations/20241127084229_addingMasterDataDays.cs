using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class addingMasterDataDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "days",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 2, "Tuesday" },
                    { 3, "Wednesday" },
                    { 4, "Thursday" },
                    { 5, "Friday" },
                    { 6, "Saturday" },
                    { 7, "Sunday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "days",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}
