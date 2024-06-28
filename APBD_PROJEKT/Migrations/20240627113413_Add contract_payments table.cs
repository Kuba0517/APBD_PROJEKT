using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_PROJEKT.Migrations
{
    /// <inheritdoc />
    public partial class Addcontract_paymentstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "contracts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "contracts",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "client_type",
                table: "clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.CreateTable(
                name: "contracts_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_id = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    payment_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts_payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_contracts_payments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "address", "client_type", "email", "is_deleted", "name", "pesel", "phone_number", "surname" },
                values: new object[,]
                {
                    { 1, "Address 1", 0, "individual1@example.com", false, "John", "12345678901", "123456789", "Doe" },
                    { 2, "Address 2", 0, "individual2@example.com", false, "Jane", "12345678902", "123456780", "Doe" },
                    { 3, "Address 3", 0, "individual3@example.com", false, "Alice", "12345678903", "123456781", "Smith" },
                    { 4, "Address 4", 0, "individual4@example.com", false, "Bob", "12345678904", "123456782", "Brown" },
                    { 5, "Address 5", 0, "individual5@example.com", false, "Charlie", "12345678905", "123456783", "Davis" }
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "address", "client_type", "company_name", "email", "krs", "phone_number" },
                values: new object[,]
                {
                    { 6, "Company Address 1", 1, "Company 1", "company1@example.com", "1234567890", "123456789" },
                    { 7, "Company Address 2", 1, "Company 2", "company2@example.com", "1234567891", "123456780" },
                    { 8, "Company Address 3", 1, "Company 3", "company3@example.com", "1234567892", "123456781" },
                    { 9, "Company Address 4", 1, "Company 4", "company4@example.com", "1234567893", "123456782" },
                    { 10, "Company Address 5", 1, "Company 5", "company5@example.com", "1234567894", "123456783" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_payments_contract_id",
                table: "contracts_payments",
                column: "contract_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contracts_payments");

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.RenameColumn(
                name: "id",
                table: "contracts",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "client_type",
                table: "clients",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "address", "client_type", "email", "is_deleted", "name", "pesel", "phone_number", "surname" },
                values: new object[,]
                {
                    { 1, "Address 1", "Individual", "individual1@example.com", false, "John", "12345678901", "123456789", "Doe" },
                    { 2, "Address 2", "Individual", "individual2@example.com", false, "Jane", "12345678902", "123456780", "Doe" },
                    { 3, "Address 3", "Individual", "individual3@example.com", false, "Alice", "12345678903", "123456781", "Smith" },
                    { 4, "Address 4", "Individual", "individual4@example.com", false, "Bob", "12345678904", "123456782", "Brown" },
                    { 5, "Address 5", "Individual", "individual5@example.com", false, "Charlie", "12345678905", "123456783", "Davis" }
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "address", "client_type", "company_name", "email", "krs", "phone_number" },
                values: new object[,]
                {
                    { 6, "Company Address 1", "Company", "Company 1", "company1@example.com", "1234567890", "123456789" },
                    { 7, "Company Address 2", "Company", "Company 2", "company2@example.com", "1234567891", "123456780" },
                    { 8, "Company Address 3", "Company", "Company 3", "company3@example.com", "1234567892", "123456781" },
                    { 9, "Company Address 4", "Company", "Company 4", "company4@example.com", "1234567893", "123456782" },
                    { 10, "Company Address 5", "Company", "Company 5", "company5@example.com", "1234567894", "123456783" }
                });
        }
    }
}
