using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Countries
    {
        public Countries()
        {
            using (var context = new RegistryPetsContext())
            {
                CountryList = new List<Country>();
                var countriesDB = context.Countries.ToList();


                foreach (var countryDB in countriesDB)
                {
                    CountryList.Add(new Country(countryDB.Id, countryDB.Name));
                }
            }
        }

        public List<Country> CountryList {
            get;
            private set; 
        }

        public Country? GetCountry(int countryId)
        {
            return CountryList.Where(x => x.Id == countryId).FirstOrDefault();
        }

    }
}
