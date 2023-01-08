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
    public class CountryController
    {
        public static List<CountryDTO> GetCountries()
        {
            var res = new List<CountryDTO>
            {
                new CountryDTO()
                {
                    Id = 0,
                    Name = ""
                }
            };
            var countries = CountryService.GetCountries();
            foreach (var country in countries)
            {
                res.Add(DTOModelConverter.ConvertCountryToDTO(country));
            }
            return res;
        }
    }
}
