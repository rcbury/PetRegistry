using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Vaccine
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public int ValidityPeriod { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; } = new List<Vaccination>();
}
