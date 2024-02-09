using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMoneda.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristCurrencyAmount",
                table: "Conversions",
                newName: "FirstCurrencyAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstCurrencyAmount",
                table: "Conversions",
                newName: "FristCurrencyAmount");
        }
    }
}
