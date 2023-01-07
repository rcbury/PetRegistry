using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class NewSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "legal_person",
                columns: new[] { "id", "address", "email", "FK_country", "FK_locality", "INN", "KPP", "name", "phone" },
                values: new object[,]
                {
                    { 1, "Республики, 1", "ivanovic@gmail.com", 1, 1, "0000000001", "1231312312", "ИвановыИвановичи", "80000000000" },
                    { 2, "Республики, 1", "ivanovic1@gmail.com", 1, 2, "0000000002", "1231312313", "ПетрыИвановичи", "80000000001" },
                    { 3, "Республики, 1", "ivanovic@gmail.com", 1, 3, "0000000003", "1231312315", "ГригорииИвановичи", "80000000002" }
                });

            migrationBuilder.InsertData(
                table: "physical_person",
                columns: new[] { "id", "address", "email", "FK_country", "FK_locality", "name", "phone" },
                values: new object[,]
                {
                    { 1, "Республики, 1", "ivanovic@gmail.com", 1, 1, "Иван Иванов Иванович", "80000000000" },
                    { 2, "Республики, 1", "ivanovic1@gmail.com", 1, 2, "Петр Иванов Иванович", "80000000001" },
                    { 3, "Республики, 1", "ivanovic@gmail.com", 1, 3, "Григорий Иванов Иванович", "80000000002" },
                    { 4, "Республики, 1", "ivanovic@gmail.com", 1, 2, "Антон Иванов Иванович", "80000000003" },
                    { 5, "Республики, 1", "ivanovic@gmail.com", 1, 1, "Алексей Иванов Иванович", "80000000004" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "legal_person",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "legal_person",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "legal_person",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "physical_person",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "physical_person",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "physical_person",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "physical_person",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "physical_person",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[] { 5, "Сотрудник приюта" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "email", "FK_location", "FK_role", "FK_shelter", "login", "name", "password" },
                values: new object[] { 5, "ivanov@gmail.com", null, 5, 1, "ivan5", "Иванов Иван Иванович", "8e5aa3866bb85289df35552106de5b21" });
        }
    }
}
