using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class UniqueChipId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal");

            migrationBuilder.AddPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal",
                columns: new[] { "date", "FK_animal", "FK_user" });

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_chip_id",
                table: "animal_card",
                column: "chip_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal");

            migrationBuilder.DropIndex(
                name: "IX_animal_card_chip_id",
                table: "animal_card");

            migrationBuilder.AddPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal",
                columns: new[] { "date", "FK_animal" });
        }
    }
}
