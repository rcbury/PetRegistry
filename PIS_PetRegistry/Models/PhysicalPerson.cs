using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class PhysicalPerson
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? FkLocality { get; set; }

    public int? FkCountry { get; set; }

    public virtual ICollection<AnimalCard> AnimalCards { get; } = new List<AnimalCard>();

    public virtual Country? FkCountryNavigation { get; set; }

    public virtual Location? FkLocalityNavigation { get; set; }
}
