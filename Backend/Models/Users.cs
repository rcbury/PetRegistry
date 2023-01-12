using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Backend.Models;

public class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Shelter? Shelter { get; set; }
}
