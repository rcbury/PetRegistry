using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Vaccination
{
    public DateOnly DateEnd { get; set; }

    public int FkAnimal { get; set; }

    public int FkUser { get; set; }

    public int FkVaccine { get; set; }

    public virtual AnimalCard FkAnimalNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;

    public virtual Vaccine FkVaccineNavigation { get; set; } = null!;
}
