using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend;

namespace PIS_PetRegistry.Models;

public partial class RegistryPetsContext : DbContext
{
    public RegistryPetsContext()
    {
    }

    public RegistryPetsContext(DbContextOptions<RegistryPetsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AmimalCardLog> AmimalCardLogs { get; set; }

    public virtual DbSet<AnimalCard> AnimalCards { get; set; }

    public virtual DbSet<AnimalCategory> AnimalCategories { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<LegalPerson> LegalPeople { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LogType> LogTypes { get; set; }

    public virtual DbSet<ParasiteTreatment> ParasiteTreatments { get; set; }

    public virtual DbSet<ParasiteTreatmentMedication> ParasiteTreatmentMedications { get; set; }

    public virtual DbSet<PhysicalPerson> PhysicalPeople { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shelter> Shelters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql("Host=localhost;Database=registry_pets;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AmimalCardLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");

            entity.ToTable("amimal_card_log");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateTime).HasColumnName("create_time");
            entity.Property(e => e.Description)
                .HasColumnType("json")
                .HasColumnName("description");
            entity.Property(e => e.FkLogsType).HasColumnName("FK_logs_type");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");

            entity.HasOne(d => d.FkLogsTypeNavigation).WithMany(p => p.AmimalCardLogs)
                .HasForeignKey(d => d.FkLogsType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_logs_type");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.AmimalCardLogs)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Entity<AnimalCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animals_pkey");

            entity.ToTable("animal_card");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ChipId)
                .HasColumnType("character varying")
                .HasColumnName("chip_id");
            entity.Property(e => e.FkCategory).HasColumnName("FK_category");
            entity.Property(e => e.FkShelter).HasColumnName("FK_shelter");
            entity.Property(e => e.IsBoy).HasColumnName("is_boy");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");

            entity.HasOne(d => d.FkCategoryNavigation).WithMany(p => p.AnimalCards)
                .HasForeignKey(d => d.FkCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_category");

            entity.HasOne(d => d.FkShelterNavigation).WithMany(p => p.AnimalCards)
                .HasForeignKey(d => d.FkShelter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_shelter");
        });

        modelBuilder.Entity<AnimalCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animal_category_pkey");

            entity.ToTable("animal_category");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.FkAnimalCard, e.FkUser, e.FkPhysicalPerson, e.Id }).HasName("contract_pkey");

            entity.ToTable("contract");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FkAnimalCard).HasColumnName("FK_animal_card");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.FkPhysicalPerson).HasColumnName("FK_physical_person");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.FkLegalPerson).HasColumnName("FK_legal_person");
            entity.Property(e => e.Number).HasColumnName("number");

            entity.HasOne(d => d.FkAnimalCardNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.FkAnimalCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal_card");

            entity.HasOne(d => d.FkLegalPersonNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.FkLegalPerson)
                .HasConstraintName("FK_legal_person");

            entity.HasOne(d => d.FkPhysicalPersonNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.FkPhysicalPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_physical_person");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<LegalPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("legal_person_pkey");

            entity.ToTable("legal_person");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FkCountry).HasColumnName("FK_country");
            entity.Property(e => e.FkLocality).HasColumnName("FK_locality");
            entity.Property(e => e.Inn)
                .HasColumnType("character varying")
                .HasColumnName("INN");
            entity.Property(e => e.Kpp)
                .HasColumnType("character varying")
                .HasColumnName("KPP");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");

            entity.HasOne(d => d.FkCountryNavigation).WithMany(p => p.LegalPeople)
                .HasForeignKey(d => d.FkCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_country");

            entity.HasOne(d => d.FkLocalityNavigation).WithMany(p => p.LegalPeople)
                .HasForeignKey(d => d.FkLocality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locality_pkey");

            entity.ToTable("location");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<LogType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("log_type_pkey");

            entity.ToTable("log_type");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<ParasiteTreatment>(entity =>
        {
            entity.HasKey(e => new { e.FkAnimal, e.FkUser, e.FkMedication, e.Date }).HasName("parasite_treatment_pkey");

            entity.ToTable("parasite_treatment");

            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.FkMedication).HasColumnName("FK_medication");
            entity.Property(e => e.Date).HasColumnName("date");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.ParasiteTreatments)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkMedicationNavigation).WithMany(p => p.ParasiteTreatments)
                .HasForeignKey(d => d.FkMedication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_medication");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.ParasiteTreatments)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Entity<ParasiteTreatmentMedication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parasite_treatment_medication_pkey");

            entity.ToTable("parasite_treatment_medication");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<PhysicalPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("physical_person_pkey");

            entity.ToTable("physical_person");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FkCountry).HasColumnName("FK_country");
            entity.Property(e => e.FkLocality).HasColumnName("FK_locality");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");

            entity.HasOne(d => d.FkCountryNavigation).WithMany(p => p.PhysicalPeople)
                .HasForeignKey(d => d.FkCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_country");

            entity.HasOne(d => d.FkLocalityNavigation).WithMany(p => p.PhysicalPeople)
                .HasForeignKey(d => d.FkLocality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shelter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shelter_pkey");

            entity.ToTable("shelter");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.FkLocation).HasColumnName("FK_location");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.FkLocationNavigation).WithMany(p => p.Shelters)
                .HasForeignKey(d => d.FkLocation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FkLocation).HasColumnName("FK_location");
            entity.Property(e => e.FkRole).HasColumnName("FK_role");
            entity.Property(e => e.FkShelter).HasColumnName("FK_shelter");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");

            entity.HasOne(d => d.FkLocationNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkLocation)
                .HasConstraintName("FK_location");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role");

            entity.HasOne(d => d.FkShelterNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkShelter)
                .HasConstraintName("FK_shelter");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => new { e.DateEnd, e.FkAnimal, e.FkUser, e.FkVaccine }).HasName("vaccination_pkey");

            entity.ToTable("vaccination");

            entity.Property(e => e.DateEnd).HasColumnName("date_end");
            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.FkVaccine).HasColumnName("FK_vaccine");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");

            entity.HasOne(d => d.FkVaccineNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.FkVaccine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vaccine");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vaccine_pkey");

            entity.ToTable("vaccine");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.ValidityPeriod).HasColumnName("validity_period");
        });

        modelBuilder.Entity<VeterinaryAppointmentAnimal>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.FkAnimal, e.FkUser }).HasName("veterinary_appointment_animal_pkey");

            entity.ToTable("veterinary_appointment_animal");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.IsCompleted).HasColumnName("is_completed");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.VeterinaryAppointmentAnimals)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.VeterinaryAppointmentAnimals)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Seed();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
