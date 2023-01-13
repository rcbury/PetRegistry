using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Locations
    {
        public Locations()
        {
            using (var context = new RegistryPetsContext())
            {
                LocationsList = new List<Location>();
                
                var locationsDB = context.Locations.ToList();

                foreach (var locationDB in locationsDB)
                {
                    LocationsList.Add(new Location(locationDB.Id, locationDB.Name));
                }
            }
        }

        public List<Location> LocationsList { get; private set; }

        public Location? GetLocation(int locationId)
        {
            return LocationsList.Where(x => x.Id == locationId).FirstOrDefault();
        }
    }
}
