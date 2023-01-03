using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Contract
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public DateOnly Date { get; set; }

    public int FkAnimalCard { get; set; }

    public int FkUser { get; set; }

    public int FkPhysicalPerson { get; set; }

    public int? FkLegalPerson { get; set; }

    public virtual AnimalCard FkAnimalCardNavigation { get; set; } = null!;

    public virtual LegalPerson? FkLegalPersonNavigation { get; set; }

    public virtual PhysicalPerson FkPhysicalPersonNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
