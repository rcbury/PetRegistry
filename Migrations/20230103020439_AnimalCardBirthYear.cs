using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class AnimalCardBirthYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfBirth",
                table: "animal_card",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                table: "animal_card");
        }
    }
}
