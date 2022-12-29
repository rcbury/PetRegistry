using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class AnimalCard
{
    public int Id { get; set; }

    public bool IsBoy { get; set; }

    public string Name { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public int FkCategory { get; set; }

    public int FkShelter { get; set; }

    public string ChipId { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual AnimalCategory FkCategoryNavigation { get; set; } = null!;

    public virtual Shelter FkShelterNavigation { get; set; } = null!;

    public virtual ICollection<ParasiteTreatment> ParasiteTreatments { get; } = new List<ParasiteTreatment>();

    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();

    public virtual ICollection<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; } = new List<VeterinaryAppointmentAnimal>();
}
