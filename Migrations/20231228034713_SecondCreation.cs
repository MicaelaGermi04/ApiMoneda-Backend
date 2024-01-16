using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiMoneda.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "Symbol", "Value" },
                values: new object[,]
                {
                    { 1, "Peso argentino", "$", 0.0012m },
                    { 2, "Dolar estadounidense", "$", 1m },
                    { 3, "Euro", "€", 1.11m },
                    { 4, "Libra esterlina", "£", 1.28m },
                    { 5, "Yen", "¥", 0.0071m },
                    { 6, "Dolar canadiense", "$", 0.76m },
                    { 7, "Dolar australiano", "CHF", 0.69m },
                    { 8, "Franco suizo", "$", 1.19m },
                    { 9, "Yuan chino", "¥", 0.14m },
                    { 10, "Peso mexicano", "$", 0.059m },
                    { 11, "Peso uruguayo", "$", 0.026m },
                    { 12, "Peso chileno", "$", 0.0011m },
                    { 13, "Real", "R$", 0.21m }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AmountOfConvertions", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 10, "Free", 0.0 },
                    { 2, 100, "Trial", 0.80000000000000004 },
                    { 3, 999999999, "Pro", 1.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
