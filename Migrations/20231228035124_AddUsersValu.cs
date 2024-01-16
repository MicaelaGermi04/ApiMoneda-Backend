using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiMoneda.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersValu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Role", "SubscriptionId", "UserName" },
                values: new object[,]
                {
                    { 1, "micaela@mail.com", "Micaela", "Germi", "mica123", 0, 3, "MicaG" },
                    { 2, "Juan@mail.com", "Juan", "Perez", "juan123", 1, 2, "Juancito" },
                    { 3, "Maria@mail.com", "Maria", "Lopez", "maria123", 1, 1, "MariaJ" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
