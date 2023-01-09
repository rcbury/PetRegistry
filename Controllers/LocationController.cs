using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Services;
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
            var res = new List<LocationDTO>
            {
                new LocationDTO()
                {
                    Id = 0,
                    Name = ""
                }
            };
            var locations = LocationService.GetLocations();
            foreach (var location in locations)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(location));
            }
            return res;
        }
    }
}
