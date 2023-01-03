using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "animal_category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Собака/пёс" },
                    { 2, "Кошка/кот" }
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "РФ" },
                    { 2, "Узбекистан" },
                    { 3, "Казахстан" },
                    { 4, "Китай" }
                });

            migrationBuilder.InsertData(
                table: "location",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Тюмень" },
                    { 2, "Тобольск" },
                    { 3, "Омск" }
                });

            migrationBuilder.InsertData(
                table: "log_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Добавление" },
                    { 2, "Изменение" },
                    { 3, "Удаление" }
                });

            migrationBuilder.InsertData(
                table: "parasite_treatment_medication",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Препарат для дегельминтизации" },
                    { 2, "Препарат для обработки от эктопаразитов" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Ветврач" },
                    { 2, "Оператор приюта" },
                    { 3, "Сотрудник ветслужбы" },
                    { 4, "Сотрудник ОМСУ" },
                    { 5, "Сотрудник приюта" }
                });

            migrationBuilder.InsertData(
                table: "vaccine",
                columns: new[] { "id", "name", "number", "validity_period" },
                values: new object[,]
                {
                    { 1, "Вакцина против бешенества", 101777, 6 },
                    { 2, "Вакцина против чумы", 102771, 6 }
                });

            migrationBuilder.InsertData(
                table: "shelter",
                columns: new[] { "id", "address", "FK_location", "name" },
                values: new object[,]
                {
                    { 1, "Республики, 1", 1, "Сытая морда" },
                    { 2, "​Семёна Ремезова, 123/2", 2, "Счастливый ушастик" },
                    { 3, "Рабиновича, 69а", 3, "Умка" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "email", "FK_location", "FK_role", "FK_shelter", "login", "name", "password" },
                values: new object[,]
                {
                    { 3, "petrov@gmail.com", null, 3, null, "petr3", "Петров Петр Петрович", "b5f44ae34083deccec95df26067e02a0" },
                    { 4, "sidorov@gmail.com", 1, 4, null, "gena4", "Сидоров Геннадий Иванович", "b5f44ae34083deccec95df26067e02a0" },
                    { 1, "mikhailivanovic@gmail.com", null, 1, 1, "mikhail1", "Михайлов Михаил Иванович", "b5f44ae34083deccec95df26067e02a0" },
                    { 2, "lenaivanova@gmail.com", null, 2, 1, "elena2", "Михайлова Елена Ивановна", "f7373d2b16a4a61a2ee9e9aa9a4b2bee" },
                    { 5, "ivanov@gmail.com", null, 5, 1, "ivan5", "Иванов Иван Иванович", "b5f44ae34083deccec95df26067e02a0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "animal_category",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "animal_category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "log_type",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "log_type",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "log_type",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "parasite_treatment_medication",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "parasite_treatment_medication",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "shelter",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "shelter",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "vaccine",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "vaccine",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "location",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "location",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "shelter",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "location",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
