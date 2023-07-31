using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixUsersCostPriceSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "usersCosts",
                type: "decimal(12,4)",
                precision: 12,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)",
                oldPrecision: 8,
                oldScale: 6);

            migrationBuilder.AlterColumn<int>(
                name: "currencyCode",
                table: "usersCosts",
                type: "int",
                nullable: false,
                defaultValue: 12,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "usersCosts",
                type: "decimal(8,6)",
                precision: 8,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)",
                oldPrecision: 12,
                oldScale: 4);

            migrationBuilder.AlterColumn<int>(
                name: "currencyCode",
                table: "usersCosts",
                type: "int",
                nullable: false,
                defaultValue: 11,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 12);
        }
    }
}
