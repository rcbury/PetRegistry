using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Shelter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? FkLocality { get; set; }

    public virtual AnimalCard? AnimalCard { get; set; }

    public virtual Location? FkLocalityNavigation { get; set; }

    public virtual ICollection<Log> Logs { get; } = new List<Log>();
}
