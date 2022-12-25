using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class AnimalCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AnimalCard> AnimalCards { get; } = new List<AnimalCard>();
}
