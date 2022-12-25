using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PISPetRegistry.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "animal_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("animal_category_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("country_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("locality_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("log_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    INN = table.Column<string>(type: "character varying", nullable: true),
                    KPP = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    address = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    FKcountry = table.Column<int>(name: "FK_country", type: "integer", nullable: true),
                    FKlocality = table.Column<int>(name: "FK_locality", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("legal_person_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_country",
                        column: x => x.FKcountry,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_locality",
                        column: x => x.FKlocality,
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "physical_persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    address = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    FKlocality = table.Column<int>(name: "FK_locality", type: "integer", nullable: true),
                    FKcountry = table.Column<int>(name: "FK_country", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("physical_person_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_country",
                        column: x => x.FKcountry,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_locality",
                        column: x => x.FKlocality,
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shelters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    address = table.Column<string>(type: "character varying", nullable: true),
                    FKlocality = table.Column<int>(name: "FK_locality", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shelter_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_locality",
                        column: x => x.FKlocality,
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    login = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    FKrole = table.Column<int>(name: "FK_role", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_role",
                        column: x => x.FKrole,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "animal_card",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    chipid = table.Column<Guid>(name: "chip_id", type: "uuid", nullable: false),
                    sex = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    photo = table.Column<string>(type: "character varying", nullable: true),
                    nametrapingservice = table.Column<string>(name: "name_traping_service", type: "character varying", nullable: false),
                    iscontract = table.Column<bool>(name: "is_contract", type: "boolean", nullable: true),
                    FKcategory = table.Column<int>(name: "FK_category", type: "integer", nullable: true),
                    FKlegalperson = table.Column<int>(name: "FK_legal_person", type: "integer", nullable: true),
                    FKphysicalperson = table.Column<int>(name: "FK_physical_person", type: "integer", nullable: true),
                    FKshelter = table.Column<int>(name: "FK_shelter", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("animals_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_category",
                        column: x => x.FKcategory,
                        principalTable: "animal_category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_legal_person",
                        column: x => x.FKlegalperson,
                        principalTable: "legal_persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_physical_person",
                        column: x => x.FKphysicalperson,
                        principalTable: "physical_persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_shelter",
                        column: x => x.id,
                        principalTable: "shelters",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    data = table.Column<string>(type: "json", nullable: true),
                    createtime = table.Column<DateOnly>(name: "create_time", type: "date", nullable: true),
                    FKlogstype = table.Column<int>(name: "FK_logs_type", type: "integer", nullable: false),
                    FKshelter = table.Column<int>(name: "FK_shelter", type: "integer", nullable: false),
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
                        name: "FK_shelter",
                        column: x => x.FKshelter,
                        principalTable: "shelters",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "treatment_parasites_animal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    dateevent = table.Column<DateOnly>(name: "date_event", type: "date", nullable: true),
                    namemedicines = table.Column<string>(name: "name_medicines", type: "character varying", nullable: true),
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("treatment_parasites_animal_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    dateevent = table.Column<DateOnly>(name: "date_event", type: "date", nullable: false),
                    dateendevent = table.Column<DateOnly>(name: "date_end_event", type: "date", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    numberseries = table.Column<int>(name: "number_series", type: "integer", nullable: true),
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccinations_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "veterinary_appointment_animal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    dateevent = table.Column<DateOnly>(name: "date_event", type: "date", nullable: true),
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("veterinary_appointment_animal_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_FK_category",
                table: "animal_card",
                column: "FK_category");

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_FK_legal_person",
                table: "animal_card",
                column: "FK_legal_person");

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_FK_physical_person",
                table: "animal_card",
                column: "FK_physical_person");

            migrationBuilder.CreateIndex(
                name: "IX_legal_persons_FK_country",
                table: "legal_persons",
                column: "FK_country");

            migrationBuilder.CreateIndex(
                name: "IX_legal_persons_FK_locality",
                table: "legal_persons",
                column: "FK_locality");

            migrationBuilder.CreateIndex(
                name: "IX_logs_FK_logs_type",
                table: "logs",
                column: "FK_logs_type");

            migrationBuilder.CreateIndex(
                name: "IX_logs_FK_shelter",
                table: "logs",
                column: "FK_shelter");

            migrationBuilder.CreateIndex(
                name: "IX_logs_FK_user",
                table: "logs",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_physical_persons_FK_country",
                table: "physical_persons",
                column: "FK_country");

            migrationBuilder.CreateIndex(
                name: "IX_physical_persons_FK_locality",
                table: "physical_persons",
                column: "FK_locality");

            migrationBuilder.CreateIndex(
                name: "IX_shelters_FK_locality",
                table: "shelters",
                column: "FK_locality");

            migrationBuilder.CreateIndex(
                name: "IX_treatment_parasites_animal_FK_animal",
                table: "treatment_parasites_animal",
                column: "FK_animal");

            migrationBuilder.CreateIndex(
                name: "IX_treatment_parasites_animal_FK_user",
                table: "treatment_parasites_animal",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_users_FK_role",
                table: "users",
                column: "FK_role");

            migrationBuilder.CreateIndex(
                name: "IX_vaccinations_FK_animal",
                table: "vaccinations",
                column: "FK_animal");

            migrationBuilder.CreateIndex(
                name: "IX_vaccinations_FK_user",
                table: "vaccinations",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_veterinary_appointment_animal_FK_animal",
                table: "veterinary_appointment_animal",
                column: "FK_animal");

            migrationBuilder.CreateIndex(
                name: "IX_veterinary_appointment_animal_FK_user",
                table: "veterinary_appointment_animal",
                column: "FK_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "treatment_parasites_animal");

            migrationBuilder.DropTable(
                name: "vaccinations");

            migrationBuilder.DropTable(
                name: "veterinary_appointment_animal");

            migrationBuilder.DropTable(
                name: "log_type");

            migrationBuilder.DropTable(
                name: "animal_card");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "animal_category");

            migrationBuilder.DropTable(
                name: "legal_persons");

            migrationBuilder.DropTable(
                name: "physical_persons");

            migrationBuilder.DropTable(
                name: "shelters");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
