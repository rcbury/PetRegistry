using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int FkRole { get; set; }

    public int? FkShelter { get; set; }

    public int? FkLocation { get; set; }

    public virtual ICollection<AmimalCardLog> AmimalCardLogs { get; } = new List<AmimalCardLog>();

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual Location? FkLocationNavigation { get; set; }

    public virtual Role FkRoleNavigation { get; set; } = null!;

    public virtual Shelter? FkShelterNavigation { get; set; }

    public virtual ICollection<ParasiteTreatment> ParasiteTreatments { get; } = new List<ParasiteTreatment>();

    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();

    public virtual ICollection<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; } = new List<VeterinaryAppointmentAnimal>();
}
