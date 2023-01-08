using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class CountryController
    {
        public static List<CountryDTO> GetCountries()
        {
            var countries = new List<CountryDTO>();
            countries.Add(new CountryDTO()
            {
                Id = 0,
                Name = ""
            });
            using (var context = new RegistryPetsContext())
            {
                foreach (var country in context.Countries.ToList())
                {
                    countries.Add(new CountryDTO()
                    {
                        Id = country.Id,
                        Name = country.Name
                    });
                }
            }
            return countries;
        }
    }
}
