using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Backend.Models;

public class User
{
    public User(Location location, Shelter shelter, PIS_PetRegistry.Models.User userDB)
    {
        Id = userDB.Id;
        Login = userDB.Login;
        Password = userDB.Password;
        Name = userDB.Name;
        Email = userDB.Email;
        Location = location;
        Shelter = shelter;
        FkRole = userDB.FkRole;
    }

    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Location? Location { get; set; }

    public int FkRole { get; set; }

    public Shelter? Shelter { get; set; }
}
