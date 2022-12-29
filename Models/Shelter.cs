using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Shelter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int FkLocation { get; set; }

    public virtual ICollection<AnimalCard> AnimalCards { get; } = new List<AnimalCard>();

    public virtual Location FkLocationNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
