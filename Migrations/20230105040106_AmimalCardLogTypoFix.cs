using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class AmimalCardLogTypoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amimal_card_log");

            migrationBuilder.CreateTable(
                name: "animal_card_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    description = table.Column<string>(type: "json", nullable: false),
                    createtime = table.Column<DateOnly>(name: "create_time", type: "date", nullable: false),
                    FKlogstype = table.Column<int>(name: "FK_logs_type", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("logs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_type",
                        column: x => x.FKlogstype,
                        principalTable: "log_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_log_FK_logs_type",
                table: "animal_card_log",
                column: "FK_logs_type");

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_log_FK_user",
                table: "animal_card_log",
                column: "FK_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animal_card_log");

            migrationBuilder.CreateTable(
                name: "amimal_card_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    FKlogstype = table.Column<int>(name: "FK_logs_type", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    createtime = table.Column<DateOnly>(name: "create_time", type: "date", nullable: false),
                    description = table.Column<string>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("logs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_type",
                        column: x => x.FKlogstype,
                        principalTable: "log_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_amimal_card_log_FK_logs_type",
                table: "amimal_card_log",
                column: "FK_logs_type");

            migrationBuilder.CreateIndex(
                name: "IX_amimal_card_log_FK_user",
                table: "amimal_card_log",
                column: "FK_user");
        }
    }
}
