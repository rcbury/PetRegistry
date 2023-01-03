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
                name: "location",
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
                name: "parasite_treatment_medication",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("parasite_treatment_medication_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
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
                name: "vaccine",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    validityperiod = table.Column<int>(name: "validity_period", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccine_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    INN = table.Column<string>(type: "character varying", nullable: false),
                    KPP = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    address = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    FKcountry = table.Column<int>(name: "FK_country", type: "integer", nullable: false),
                    FKlocality = table.Column<int>(name: "FK_locality", type: "integer", nullable: false)
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
                        principalTable: "location",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "physical_person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    phone = table.Column<string>(type: "character varying", nullable: false),
                    address = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    FKlocality = table.Column<int>(name: "FK_locality", type: "integer", nullable: false),
                    FKcountry = table.Column<int>(name: "FK_country", type: "integer", nullable: false)
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
                        principalTable: "location",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shelter",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    address = table.Column<string>(type: "character varying", nullable: false),
                    FKlocation = table.Column<int>(name: "FK_location", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shelter_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_locality",
                        column: x => x.FKlocation,
                        principalTable: "location",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "animal_card",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    isboy = table.Column<bool>(name: "is_boy", type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    photo = table.Column<string>(type: "character varying", nullable: false),
                    FKcategory = table.Column<int>(name: "FK_category", type: "integer", nullable: false),
                    FKshelter = table.Column<int>(name: "FK_shelter", type: "integer", nullable: false),
                    chipid = table.Column<string>(name: "chip_id", type: "character varying", nullable: false)
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
                        name: "FK_shelter",
                        column: x => x.FKshelter,
                        principalTable: "shelter",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    login = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    FKrole = table.Column<int>(name: "FK_role", type: "integer", nullable: false),
                    FKshelter = table.Column<int>(name: "FK_shelter", type: "integer", nullable: true),
                    FKlocation = table.Column<int>(name: "FK_location", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_location",
                        column: x => x.FKlocation,
                        principalTable: "location",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role",
                        column: x => x.FKrole,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_shelter",
                        column: x => x.FKshelter,
                        principalTable: "shelter",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "amimal_card_log",
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

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    FKanimalcard = table.Column<int>(name: "FK_animal_card", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    FKphysicalperson = table.Column<int>(name: "FK_physical_person", type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: true),
                    FKlegalperson = table.Column<int>(name: "FK_legal_person", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contract_pkey", x => new { x.date, x.FKanimalcard, x.FKuser, x.FKphysicalperson, x.id });
                    table.ForeignKey(
                        name: "FK_animal_card",
                        column: x => x.FKanimalcard,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_legal_person",
                        column: x => x.FKlegalperson,
                        principalTable: "legal_person",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_physical_person",
                        column: x => x.FKphysicalperson,
                        principalTable: "physical_person",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "parasite_treatment",
                columns: table => new
                {
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    FKmedication = table.Column<int>(name: "FK_medication", type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("parasite_treatment_pkey", x => new { x.FKanimal, x.FKuser, x.FKmedication, x.date });
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_medication",
                        column: x => x.FKmedication,
                        principalTable: "parasite_treatment_medication",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vaccination",
                columns: table => new
                {
                    dateend = table.Column<DateOnly>(name: "date_end", type: "date", nullable: false),
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    FKvaccine = table.Column<int>(name: "FK_vaccine", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccination_pkey", x => new { x.dateend, x.FKanimal, x.FKuser, x.FKvaccine });
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user",
                        column: x => x.FKuser,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vaccine",
                        column: x => x.FKvaccine,
                        principalTable: "vaccine",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "veterinary_appointment_animal",
                columns: table => new
                {
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    FKanimal = table.Column<int>(name: "FK_animal", type: "integer", nullable: false),
                    FKuser = table.Column<int>(name: "FK_user", type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    iscompleted = table.Column<bool>(name: "is_completed", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("veterinary_appointment_animal_pkey", x => new { x.date, x.FKanimal, x.FKuser });
                    table.ForeignKey(
                        name: "FK_animal",
                        column: x => x.FKanimal,
                        principalTable: "animal_card",
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

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_FK_category",
                table: "animal_card",
                column: "FK_category");

            migrationBuilder.CreateIndex(
                name: "IX_animal_card_FK_shelter",
                table: "animal_card",
                column: "FK_shelter");

            migrationBuilder.CreateIndex(
                name: "IX_contract_FK_animal_card",
                table: "contract",
                column: "FK_animal_card");

            migrationBuilder.CreateIndex(
                name: "IX_contract_FK_legal_person",
                table: "contract",
                column: "FK_legal_person");

            migrationBuilder.CreateIndex(
                name: "IX_contract_FK_physical_person",
                table: "contract",
                column: "FK_physical_person");

            migrationBuilder.CreateIndex(
                name: "IX_contract_FK_user",
                table: "contract",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_legal_person_FK_country",
                table: "legal_person",
                column: "FK_country");

            migrationBuilder.CreateIndex(
                name: "IX_legal_person_FK_locality",
                table: "legal_person",
                column: "FK_locality");

            migrationBuilder.CreateIndex(
                name: "IX_parasite_treatment_FK_medication",
                table: "parasite_treatment",
                column: "FK_medication");

            migrationBuilder.CreateIndex(
                name: "IX_parasite_treatment_FK_user",
                table: "parasite_treatment",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_physical_person_FK_country",
                table: "physical_person",
                column: "FK_country");

            migrationBuilder.CreateIndex(
                name: "IX_physical_person_FK_locality",
                table: "physical_person",
                column: "FK_locality");

            migrationBuilder.CreateIndex(
                name: "IX_shelter_FK_location",
                table: "shelter",
                column: "FK_location");

            migrationBuilder.CreateIndex(
                name: "IX_user_FK_location",
                table: "user",
                column: "FK_location");

            migrationBuilder.CreateIndex(
                name: "IX_user_FK_role",
                table: "user",
                column: "FK_role");

            migrationBuilder.CreateIndex(
                name: "IX_user_FK_shelter",
                table: "user",
                column: "FK_shelter");

            migrationBuilder.CreateIndex(
                name: "IX_vaccination_FK_animal",
                table: "vaccination",
                column: "FK_animal");

            migrationBuilder.CreateIndex(
                name: "IX_vaccination_FK_user",
                table: "vaccination",
                column: "FK_user");

            migrationBuilder.CreateIndex(
                name: "IX_vaccination_FK_vaccine",
                table: "vaccination",
                column: "FK_vaccine");

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
                name: "amimal_card_log");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "parasite_treatment");

            migrationBuilder.DropTable(
                name: "vaccination");

            migrationBuilder.DropTable(
                name: "veterinary_appointment_animal");

            migrationBuilder.DropTable(
                name: "log_type");

            migrationBuilder.DropTable(
                name: "legal_person");

            migrationBuilder.DropTable(
                name: "physical_person");

            migrationBuilder.DropTable(
                name: "parasite_treatment_medication");

            migrationBuilder.DropTable(
                name: "vaccine");

            migrationBuilder.DropTable(
                name: "animal_card");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "animal_category");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "shelter");

            migrationBuilder.DropTable(
                name: "location");
        }
    }
}
