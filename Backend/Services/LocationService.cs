using PIS_PetRegistry.Models;

namespace PIS_PetRegistry.Backend.Services
{
    public class LocationService
    {
        public static string GetLocationNameById(int locationId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.Locations.Where(location => location.Id == locationId).FirstOrDefault().Name;
            }
        }

        public static List<Location> GetLocations()
        {
            using (var context = new RegistryPetsContext())
            {
                return context.Locations.ToList();
            }
        }
    }
}
