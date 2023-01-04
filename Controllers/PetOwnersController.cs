using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PIS_PetRegistry.Controllers
{
    internal class PetOwnersController
    {
        public static List<PhysicalPersonDTO> GetPhysicalPeople()
        {
            var physicalPeople = new List<PhysicalPersonDTO>();
            using (var context = new RegistryPetsContext()) 
            {
                foreach (var personInfo in context.PhysicalPeople.ToList()) 
                {
                    physicalPeople.Add(new PhysicalPersonDTO()
                    {
                        Id = personInfo.Id,
                        Name = personInfo.Name,
                        Phone = personInfo.Phone,
                        Address = personInfo.Address,
                        Email = personInfo.Email,
                        FkLocality = personInfo.FkLocality,
                        FkCountry = personInfo.FkCountry
                    });
                }
            }
            return physicalPeople;
        }

        public static List<LegalPersonDTO> GetLegalPeople()
        {
            var legalPeople = new List<LegalPersonDTO>();
            using (var context = new RegistryPetsContext())
            {
                foreach (var personInfo in context.LegalPeople.ToList())
                {
                    legalPeople.Add(new LegalPersonDTO()
                    {
                        Id = personInfo.Id,
                        Inn = personInfo.Inn,
                        Kpp = personInfo.Kpp,
                        Name = personInfo.Name,
                        Phone = personInfo.Phone,
                        Address = personInfo.Address,
                        Email = personInfo.Email,
                        FkLocality = personInfo.FkLocality,
                        FkCountry = personInfo.FkCountry
                    });
                }
            }
            return legalPeople;
        }

        public static List<LocationDTO> GetLocations()
        {
            var locations = new List<LocationDTO>();
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

        public static List<CountryDTO> GetCountries() 
        {
            var countries = new List<CountryDTO>();
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
