using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class AnimalCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual IEnumerable<AnimalCard> AnimalCards { get; } = new List<AnimalCard>();

}
