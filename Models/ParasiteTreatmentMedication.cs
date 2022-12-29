using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class ParasiteTreatmentMedication
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ParasiteTreatment> ParasiteTreatments { get; } = new List<ParasiteTreatment>();
}
