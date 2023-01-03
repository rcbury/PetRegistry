using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class VeterinaryAppointmentAnimal
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int FkAnimal { get; set; }

    public int FkUser { get; set; }

    public string Name { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public virtual AnimalCard FkAnimalNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
