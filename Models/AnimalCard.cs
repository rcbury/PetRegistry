using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PIS_PetRegistry.Models;

public partial class AnimalCard
{
    public int Id { get; set; }

    public bool IsBoy { get; set; }

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }

    public int? YearOfBirth { get; set; }

    public int FkCategory { get; set; }

    public int FkShelter { get; set; }

    public string ChipId { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
    
    [JsonIgnore]
    public virtual AnimalCategory FkCategoryNavigation { get; set; } = null!;
    
    [JsonIgnore]
    public virtual Shelter FkShelterNavigation { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<ParasiteTreatment> ParasiteTreatments { get; } = new List<ParasiteTreatment>();
    
    [JsonIgnore]
    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();
    
    [JsonIgnore]
    public virtual ICollection<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; } = new List<VeterinaryAppointmentAnimal>();
}
