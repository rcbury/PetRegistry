using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class AnimalCard
{
    public int Id { get; set; }

    public Guid ChipId { get; set; }

    public bool Sex { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public string NameTrapingService { get; set; } = null!;

    public bool? IsContract { get; set; }

    public int? FkCategory { get; set; }

    public int? FkLegalPerson { get; set; }

    public int? FkPhysicalPerson { get; set; }

    public int? FkShelter { get; set; }

    public virtual AnimalCategory? FkCategoryNavigation { get; set; }

    public virtual LegalPerson? FkLegalPersonNavigation { get; set; }

    public virtual PhysicalPerson? FkPhysicalPersonNavigation { get; set; }

    public virtual Shelter IdNavigation { get; set; } = null!;

    public virtual ICollection<TreatmentParasitesAnimal> TreatmentParasitesAnimals { get; } = new List<TreatmentParasitesAnimal>();

    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();

    public virtual ICollection<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; } = new List<VeterinaryAppointmentAnimal>();
}
