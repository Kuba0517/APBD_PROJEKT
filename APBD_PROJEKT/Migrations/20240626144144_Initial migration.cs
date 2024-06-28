using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_PROJEKT.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    krs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "softwares",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    current_version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    software_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_softwares", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    software_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_paid = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    support_years = table.Column<int>(type: "int", nullable: false),
                    discount_id = table.Column<int>(type: "int", nullable: true),
                    software_version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_signed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contracts_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contracts_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_contracts_softwares_software_id",
                        column: x => x.software_id,
                        principalTable: "softwares",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "discounts",
                columns: new[] { "id", "end_date", "name", "start_date", "type", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Year Discount", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 20.0 },
                    { 2, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Subscription Discount", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 15.0 }
                });

            migrationBuilder.InsertData(
                table: "softwares",
                columns: new[] { "id", "current_version", "description", "name", "software_type" },
                values: new object[,]
                {
                    { 1, "1.0.0", "Software for managing finances", "Finance Manager", 0 },
                    { 2, "2.3.1", "Educational software for students", "EduLearn", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_client_id",
                table: "contracts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_discount_id",
                table: "contracts",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_software_id",
                table: "contracts",
                column: "software_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "softwares");
        }
    }
}
