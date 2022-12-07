﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PIS_PetRegistry.Models;

#nullable disable

namespace PISPetRegistry.Migrations
{
    [DbContext(typeof(RegistryPetsContext))]
    [Migration("20221206160620_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PIS_PetRegistry.Models.AnimalCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ChipId")
                        .HasColumnType("uuid")
                        .HasColumnName("chip_id");

                    b.Property<int?>("FkCategory")
                        .HasColumnType("integer")
                        .HasColumnName("FK_category");

                    b.Property<int?>("FkLegalPerson")
                        .HasColumnType("integer")
                        .HasColumnName("FK_legal_person");

                    b.Property<int?>("FkPhysicalPerson")
                        .HasColumnType("integer")
                        .HasColumnName("FK_physical_person");

                    b.Property<int?>("FkShelter")
                        .HasColumnType("integer")
                        .HasColumnName("FK_shelter");

                    b.Property<bool?>("IsContract")
                        .HasColumnType("boolean")
                        .HasColumnName("is_contract");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("NameTrapingService")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("name_traping_service");

                    b.Property<string>("Photo")
                        .HasColumnType("character varying")
                        .HasColumnName("photo");

                    b.Property<bool>("Sex")
                        .HasColumnType("boolean")
                        .HasColumnName("sex");

                    b.HasKey("Id")
                        .HasName("animals_pkey");

                    b.HasIndex("FkCategory");

                    b.HasIndex("FkLegalPerson");

                    b.HasIndex("FkPhysicalPerson");

                    b.ToTable("animal_card", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.AnimalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("animal_category_pkey");

                    b.ToTable("animal_category", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("country_pkey");

                    b.ToTable("countries", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.LegalPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("character varying")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<int?>("FkCountry")
                        .HasColumnType("integer")
                        .HasColumnName("FK_country");

                    b.Property<int?>("FkLocality")
                        .HasColumnType("integer")
                        .HasColumnName("FK_locality");

                    b.Property<string>("Inn")
                        .HasColumnType("character varying")
                        .HasColumnName("INN");

                    b.Property<string>("Kpp")
                        .HasColumnType("character varying")
                        .HasColumnName("KPP");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("character varying")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("legal_person_pkey");

                    b.HasIndex("FkCountry");

                    b.HasIndex("FkLocality");

                    b.ToTable("legal_persons", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("locality_pkey");

                    b.ToTable("locations", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("CreateTime")
                        .HasColumnType("date")
                        .HasColumnName("create_time");

                    b.Property<string>("Data")
                        .HasColumnType("json")
                        .HasColumnName("data");

                    b.Property<int>("FkLogsType")
                        .HasColumnType("integer")
                        .HasColumnName("FK_logs_type");

                    b.Property<int>("FkShelter")
                        .HasColumnType("integer")
                        .HasColumnName("FK_shelter");

                    b.Property<int>("FkUser")
                        .HasColumnType("integer")
                        .HasColumnName("FK_user");

                    b.HasKey("Id")
                        .HasName("logs_pkey");

                    b.HasIndex("FkLogsType");

                    b.HasIndex("FkShelter");

                    b.HasIndex("FkUser");

                    b.ToTable("logs", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.LogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("log_type_pkey");

                    b.ToTable("log_type", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.PhysicalPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("character varying")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<int?>("FkCountry")
                        .HasColumnType("integer")
                        .HasColumnName("FK_country");

                    b.Property<int?>("FkLocality")
                        .HasColumnType("integer")
                        .HasColumnName("FK_locality");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("character varying")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("physical_person_pkey");

                    b.HasIndex("FkCountry");

                    b.HasIndex("FkLocality");

                    b.ToTable("physical_persons", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("role_pkey");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Shelter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("character varying")
                        .HasColumnName("address");

                    b.Property<int?>("FkLocality")
                        .HasColumnType("integer")
                        .HasColumnName("FK_locality");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("shelter_pkey");

                    b.HasIndex("FkLocality");

                    b.ToTable("shelters", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.TreatmentParasitesAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("DateEvent")
                        .HasColumnType("date")
                        .HasColumnName("date_event");

                    b.Property<int>("FkAnimal")
                        .HasColumnType("integer")
                        .HasColumnName("FK_animal");

                    b.Property<int>("FkUser")
                        .HasColumnType("integer")
                        .HasColumnName("FK_user");

                    b.Property<string>("NameMedicines")
                        .HasColumnType("character varying")
                        .HasColumnName("name_medicines");

                    b.HasKey("Id")
                        .HasName("treatment_parasites_animal_pkey");

                    b.HasIndex("FkAnimal");

                    b.HasIndex("FkUser");

                    b.ToTable("treatment_parasites_animal", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<int?>("FkRole")
                        .HasColumnType("integer")
                        .HasColumnName("FK_role");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("user_pkey");

                    b.HasIndex("FkRole");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Vaccination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("DateEndEvent")
                        .HasColumnType("date")
                        .HasColumnName("date_end_event");

                    b.Property<DateOnly>("DateEvent")
                        .HasColumnType("date")
                        .HasColumnName("date_event");

                    b.Property<int>("FkAnimal")
                        .HasColumnType("integer")
                        .HasColumnName("FK_animal");

                    b.Property<int>("FkUser")
                        .HasColumnType("integer")
                        .HasColumnName("FK_user");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<int?>("NumberSeries")
                        .HasColumnType("integer")
                        .HasColumnName("number_series");

                    b.HasKey("Id")
                        .HasName("vaccinations_pkey");

                    b.HasIndex("FkAnimal");

                    b.HasIndex("FkUser");

                    b.ToTable("vaccinations", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.VeterinaryAppointmentAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("DateEvent")
                        .HasColumnType("date")
                        .HasColumnName("date_event");

                    b.Property<int>("FkAnimal")
                        .HasColumnType("integer")
                        .HasColumnName("FK_animal");

                    b.Property<int>("FkUser")
                        .HasColumnType("integer")
                        .HasColumnName("FK_user");

                    b.HasKey("Id")
                        .HasName("veterinary_appointment_animal_pkey");

                    b.HasIndex("FkAnimal");

                    b.HasIndex("FkUser");

                    b.ToTable("veterinary_appointment_animal", (string)null);
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.AnimalCard", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.AnimalCategory", "FkCategoryNavigation")
                        .WithMany("AnimalCards")
                        .HasForeignKey("FkCategory")
                        .HasConstraintName("FK_category");

                    b.HasOne("PIS_PetRegistry.Models.LegalPerson", "FkLegalPersonNavigation")
                        .WithMany("AnimalCards")
                        .HasForeignKey("FkLegalPerson")
                        .HasConstraintName("FK_legal_person");

                    b.HasOne("PIS_PetRegistry.Models.PhysicalPerson", "FkPhysicalPersonNavigation")
                        .WithMany("AnimalCards")
                        .HasForeignKey("FkPhysicalPerson")
                        .HasConstraintName("FK_physical_person");

                    b.HasOne("PIS_PetRegistry.Models.Shelter", "IdNavigation")
                        .WithOne("AnimalCard")
                        .HasForeignKey("PIS_PetRegistry.Models.AnimalCard", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_shelter");

                    b.Navigation("FkCategoryNavigation");

                    b.Navigation("FkLegalPersonNavigation");

                    b.Navigation("FkPhysicalPersonNavigation");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.LegalPerson", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.Country", "FkCountryNavigation")
                        .WithMany("LegalPeople")
                        .HasForeignKey("FkCountry")
                        .HasConstraintName("FK_country");

                    b.HasOne("PIS_PetRegistry.Models.Location", "FkLocalityNavigation")
                        .WithMany("LegalPeople")
                        .HasForeignKey("FkLocality")
                        .HasConstraintName("FK_locality");

                    b.Navigation("FkCountryNavigation");

                    b.Navigation("FkLocalityNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Log", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.LogType", "FkLogsTypeNavigation")
                        .WithMany("Logs")
                        .HasForeignKey("FkLogsType")
                        .IsRequired()
                        .HasConstraintName("FK_logs_type");

                    b.HasOne("PIS_PetRegistry.Models.Shelter", "FkShelterNavigation")
                        .WithMany("Logs")
                        .HasForeignKey("FkShelter")
                        .IsRequired()
                        .HasConstraintName("FK_shelter");

                    b.HasOne("PIS_PetRegistry.Models.User", "FkUserNavigation")
                        .WithMany("Logs")
                        .HasForeignKey("FkUser")
                        .IsRequired()
                        .HasConstraintName("FK_user");

                    b.Navigation("FkLogsTypeNavigation");

                    b.Navigation("FkShelterNavigation");

                    b.Navigation("FkUserNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.PhysicalPerson", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.Country", "FkCountryNavigation")
                        .WithMany("PhysicalPeople")
                        .HasForeignKey("FkCountry")
                        .HasConstraintName("FK_country");

                    b.HasOne("PIS_PetRegistry.Models.Location", "FkLocalityNavigation")
                        .WithMany("PhysicalPeople")
                        .HasForeignKey("FkLocality")
                        .HasConstraintName("FK_locality");

                    b.Navigation("FkCountryNavigation");

                    b.Navigation("FkLocalityNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Shelter", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.Location", "FkLocalityNavigation")
                        .WithMany("Shelters")
                        .HasForeignKey("FkLocality")
                        .HasConstraintName("FK_locality");

                    b.Navigation("FkLocalityNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.TreatmentParasitesAnimal", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.AnimalCard", "FkAnimalNavigation")
                        .WithMany("TreatmentParasitesAnimals")
                        .HasForeignKey("FkAnimal")
                        .IsRequired()
                        .HasConstraintName("FK_animal");

                    b.HasOne("PIS_PetRegistry.Models.User", "FkUserNavigation")
                        .WithMany("TreatmentParasitesAnimals")
                        .HasForeignKey("FkUser")
                        .IsRequired()
                        .HasConstraintName("FK_user");

                    b.Navigation("FkAnimalNavigation");

                    b.Navigation("FkUserNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.User", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.Role", "FkRoleNavigation")
                        .WithMany("Users")
                        .HasForeignKey("FkRole")
                        .HasConstraintName("FK_role");

                    b.Navigation("FkRoleNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Vaccination", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.AnimalCard", "FkAnimalNavigation")
                        .WithMany("Vaccinations")
                        .HasForeignKey("FkAnimal")
                        .IsRequired()
                        .HasConstraintName("FK_animal");

                    b.HasOne("PIS_PetRegistry.Models.User", "FkUserNavigation")
                        .WithMany("Vaccinations")
                        .HasForeignKey("FkUser")
                        .IsRequired()
                        .HasConstraintName("FK_user");

                    b.Navigation("FkAnimalNavigation");

                    b.Navigation("FkUserNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.VeterinaryAppointmentAnimal", b =>
                {
                    b.HasOne("PIS_PetRegistry.Models.AnimalCard", "FkAnimalNavigation")
                        .WithMany("VeterinaryAppointmentAnimals")
                        .HasForeignKey("FkAnimal")
                        .IsRequired()
                        .HasConstraintName("FK_animal");

                    b.HasOne("PIS_PetRegistry.Models.User", "FkUserNavigation")
                        .WithMany("VeterinaryAppointmentAnimals")
                        .HasForeignKey("FkUser")
                        .IsRequired()
                        .HasConstraintName("FK_user");

                    b.Navigation("FkAnimalNavigation");

                    b.Navigation("FkUserNavigation");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.AnimalCard", b =>
                {
                    b.Navigation("TreatmentParasitesAnimals");

                    b.Navigation("Vaccinations");

                    b.Navigation("VeterinaryAppointmentAnimals");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.AnimalCategory", b =>
                {
                    b.Navigation("AnimalCards");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Country", b =>
                {
                    b.Navigation("LegalPeople");

                    b.Navigation("PhysicalPeople");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.LegalPerson", b =>
                {
                    b.Navigation("AnimalCards");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Location", b =>
                {
                    b.Navigation("LegalPeople");

                    b.Navigation("PhysicalPeople");

                    b.Navigation("Shelters");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.LogType", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.PhysicalPerson", b =>
                {
                    b.Navigation("AnimalCards");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.Shelter", b =>
                {
                    b.Navigation("AnimalCard");

                    b.Navigation("Logs");
                });

            modelBuilder.Entity("PIS_PetRegistry.Models.User", b =>
                {
                    b.Navigation("Logs");

                    b.Navigation("TreatmentParasitesAnimals");

                    b.Navigation("Vaccinations");

                    b.Navigation("VeterinaryAppointmentAnimals");
                });
#pragma warning restore 612, 618
        }
    }
}
