using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class ParasiteTreatment
{
    public int FkAnimal { get; set; }

    public int FkUser { get; set; }

    public int FkMedication { get; set; }

    public DateOnly Date { get; set; }

    public virtual AnimalCard FkAnimalNavigation { get; set; } = null!;

    public virtual ParasiteTreatmentMedication FkMedicationNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
