using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class LocationController
    {
        public static List<LocationDTO> GetLocations()
        {
            var locations = new List<LocationDTO>();
            locations.Add(new LocationDTO()
            {
                Id = 0,
                Name = ""
            });
            using (var context = new RegistryPetsContext())
            {
                foreach (var location in context.Locations.ToList())
                {
                    locations.Add(new LocationDTO()
                    {
                        Id = location.Id,
                        Name = location.Name
                    });
                }
            }
            return locations;
        }
    }
}
