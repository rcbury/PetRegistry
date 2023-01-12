using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Backend.Models;
public class Shelters
{
    public Shelters(Locations locations)
    {
        using (var context = new RegistryPetsContext())
        {
            ShelterList = new List<Shelter>();

            var sheltersDB = context.Shelters.ToList();

            foreach (var shelterDB in sheltersDB)
            {
                ShelterList.Add(new Shelter()
                {
                    Id = shelterDB.Id,
                    Name = shelterDB.Name,
                    Address = shelterDB.Address,
                    Location = locations.GetLocation(shelterDB.FkLocation)
                });
            }
        }
    }

    public List<Shelter> ShelterList { get; private set; }

    public Shelter? GetShelter(int locationId)
    {
        return ShelterList.Where(x => x.Id == locationId).FirstOrDefault();
    }
}
