using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<LegalPerson> LegalPeople { get; } = new List<LegalPerson>();

    public virtual ICollection<PhysicalPerson> PhysicalPeople { get; } = new List<PhysicalPerson>();
}
