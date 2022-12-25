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

    public int? FkRole { get; set; }

    public virtual Role? FkRoleNavigation { get; set; }

    public virtual ICollection<Log> Logs { get; } = new List<Log>();

    public virtual ICollection<TreatmentParasitesAnimal> TreatmentParasitesAnimals { get; } = new List<TreatmentParasitesAnimal>();

    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();

    public virtual ICollection<VeterinaryAppointmentAnimal> VeterinaryAppointmentAnimals { get; } = new List<VeterinaryAppointmentAnimal>();
}
