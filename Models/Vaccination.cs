using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Vaccination
{
    public int Id { get; set; }

    public DateOnly DateEvent { get; set; }

    public DateOnly? DateEndEvent { get; set; }

    public string? Name { get; set; }

    public int? NumberSeries { get; set; }

    public int FkAnimal { get; set; }

    public int FkUser { get; set; }

    public virtual AnimalCard FkAnimalNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
