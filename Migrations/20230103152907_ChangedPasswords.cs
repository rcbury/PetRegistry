using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "c508b76b382725a100c21e8a4d452619");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "35b4a09a4aa3bede9a833923d24d3921");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "00354d7169c4399322be98a27f553da3");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "0228a06e78a77ad502f703e3fa9ecae1");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 5,
                column: "password",
                value: "8e5aa3866bb85289df35552106de5b21");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "b5f44ae34083deccec95df26067e02a0");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "f7373d2b16a4a61a2ee9e9aa9a4b2bee");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "b5f44ae34083deccec95df26067e02a0");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "b5f44ae34083deccec95df26067e02a0");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 5,
                column: "password",
                value: "b5f44ae34083deccec95df26067e02a0");
        }
    }
}
