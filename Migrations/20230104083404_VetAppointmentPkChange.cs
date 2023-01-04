using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class VetAppointmentPkChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "veterinary_appointment_animal",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal",
                columns: new[] { "date", "FK_animal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "veterinary_appointment_animal",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "veterinary_appointment_animal_pkey",
                table: "veterinary_appointment_animal",
                columns: new[] { "date", "FK_animal", "FK_user" });
        }
    }
}
