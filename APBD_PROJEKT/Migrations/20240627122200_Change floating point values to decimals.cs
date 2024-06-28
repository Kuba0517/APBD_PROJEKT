using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_PROJEKT.Migrations
{
    /// <inheritdoc />
    public partial class Changefloatingpointvaluestodecimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "discounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "contracts_payments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 1,
                column: "value",
                value: 20m);

            migrationBuilder.UpdateData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 2,
                column: "value",
                value: 15m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "value",
                table: "discounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "value",
                table: "contracts_payments",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 1,
                column: "value",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 2,
                column: "value",
                value: 15.0);
        }
    }
}
