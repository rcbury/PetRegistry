using PIS_PetRegistry.Models;

namespace PIS_PetRegistry.Backend.Services
{
    public class LocationService
    {
        public static List<Location> GetLocations()
        {
            using (var context = new RegistryPetsContext())
            {
                return context.Locations.ToList();
            }
        }
    }
}
