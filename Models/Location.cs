using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LegalPerson> LegalPeople { get; } = new List<LegalPerson>();

    public virtual ICollection<PhysicalPerson> PhysicalPeople { get; } = new List<PhysicalPerson>();

    public virtual ICollection<Shelter> Shelters { get; } = new List<Shelter>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
