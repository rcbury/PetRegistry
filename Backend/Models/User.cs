using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Backend.Models;

public class User
{
    public User(Location location, Shelter shelter, int id, string login, string password, string name, string email, int fkRole)
    {
        Id = id;
        Login = login;
        Password = password;
        Name = name;
        Email = email;
        Location = location;
        Shelter = shelter;
        FkRole = fkRole;
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
