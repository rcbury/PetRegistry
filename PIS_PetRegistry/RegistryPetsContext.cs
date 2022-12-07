using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<AnimalCard> AnimalCards { get; set; }

    public virtual DbSet<AnimalCategory> AnimalCategories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<LegalPerson> LegalPersons { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<LogType> LogTypes { get; set; }

    public virtual DbSet<PhysicalPerson> PhysicalPersons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shelter> Shelters { get; set; }

    public virtual DbSet<TreatmentParasitesAnimal> TreatmentParasitesAnimals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    public virtual DbSet<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["ShelterDatabase"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animals_pkey");

            entity.ToTable("animal_card");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ChipId).HasColumnName("chip_id");
            entity.Property(e => e.FkCategory).HasColumnName("FK_category");
            entity.Property(e => e.FkLegalPerson).HasColumnName("FK_legal_person");
            entity.Property(e => e.FkPhysicalPerson).HasColumnName("FK_physical_person");
            entity.Property(e => e.FkShelter).HasColumnName("FK_shelter");
            entity.Property(e => e.IsContract).HasColumnName("is_contract");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.NameTrapingService)
                .HasColumnType("character varying")
                .HasColumnName("name_traping_service");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.Sex).HasColumnName("sex");

            entity.HasOne(d => d.FkCategoryNavigation).WithMany(p => p.AnimalCards)
                .HasForeignKey(d => d.FkCategory)
                .HasConstraintName("FK_category");

            entity.HasOne(d => d.FkLegalPersonNavigation).WithMany(p => p.AnimalCards)
                .HasForeignKey(d => d.FkLegalPerson)
                .HasConstraintName("FK_legal_person");

            entity.HasOne(d => d.FkPhysicalPersonNavigation).WithMany(p => p.AnimalCards)
                .HasForeignKey(d => d.FkPhysicalPerson)
                .HasConstraintName("FK_physical_person");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.AnimalCard)
                .HasForeignKey<AnimalCard>(d => d.Id)
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

            entity.ToTable("legal_persons");

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
                .HasConstraintName("FK_country");

            entity.HasOne(d => d.FkLocalityNavigation).WithMany(p => p.LegalPeople)
                .HasForeignKey(d => d.FkLocality)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locality_pkey");

            entity.ToTable("locations");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");

            entity.ToTable("logs");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateTime).HasColumnName("create_time");
            entity.Property(e => e.Data)
                .HasColumnType("json")
                .HasColumnName("data");
            entity.Property(e => e.FkLogsType).HasColumnName("FK_logs_type");
            entity.Property(e => e.FkShelter).HasColumnName("FK_shelter");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");

            entity.HasOne(d => d.FkLogsTypeNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.FkLogsType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_logs_type");

            entity.HasOne(d => d.FkShelterNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.FkShelter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_shelter");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
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

        modelBuilder.Entity<PhysicalPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("physical_person_pkey");

            entity.ToTable("physical_persons");

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
                .HasConstraintName("FK_country");

            entity.HasOne(d => d.FkLocalityNavigation).WithMany(p => p.PhysicalPeople)
                .HasForeignKey(d => d.FkLocality)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("roles");

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

            entity.ToTable("shelters");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.FkLocality).HasColumnName("FK_locality");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.FkLocalityNavigation).WithMany(p => p.Shelters)
                .HasForeignKey(d => d.FkLocality)
                .HasConstraintName("FK_locality");
        });

        modelBuilder.Entity<TreatmentParasitesAnimal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("treatment_parasites_animal_pkey");

            entity.ToTable("treatment_parasites_animal");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateEvent).HasColumnName("date_event");
            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.NameMedicines)
                .HasColumnType("character varying")
                .HasColumnName("name_medicines");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.TreatmentParasitesAnimals)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.TreatmentParasitesAnimals)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FkRole).HasColumnName("FK_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRole)
                .HasConstraintName("FK_role");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vaccinations_pkey");

            entity.ToTable("vaccinations");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateEndEvent).HasColumnName("date_end_event");
            entity.Property(e => e.DateEvent).HasColumnName("date_event");
            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.NumberSeries).HasColumnName("number_series");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        modelBuilder.Entity<VeterinaryAppointmentAnimal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veterinary_appointment_animal_pkey");

            entity.ToTable("veterinary_appointment_animal");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateEvent).HasColumnName("date_event");
            entity.Property(e => e.FkAnimal).HasColumnName("FK_animal");
            entity.Property(e => e.FkUser).HasColumnName("FK_user");

            entity.HasOne(d => d.FkAnimalNavigation).WithMany(p => p.VeterinaryAppointmentAnimals)
                .HasForeignKey(d => d.FkAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animal");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.VeterinaryAppointmentAnimals)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
