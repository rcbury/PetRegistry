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
        public static List<PhysicalPersonDTO> GetPhysicalPeople(string phone, string name, string address, string email, int country, int location)
        {
            var physicalPeopleDTO = new List<PhysicalPersonDTO>();
            using (var context = new RegistryPetsContext()) 
            {
                var physicalPeople = context.PhysicalPeople.ToList();
                if (phone != null && phone != "") 
                {
                    physicalPeople = physicalPeople.Where(person => person.Phone.Contains(phone)).ToList();
                }
                if (name != null && name != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Name.Contains(name)).ToList();
                }
                if (address != null && address != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Address.Contains(address)).ToList();
                }
                if (email != null && email != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Email.Contains(email)).ToList();
                }
                if (country != 0)
                {
                    physicalPeople = physicalPeople.Where(person => person.FkCountry == country).ToList();
                }
                if (location != 0)
                {
                    physicalPeople = physicalPeople.Where(person => person.FkLocality == location).ToList();
                }
                foreach (var personInfo in physicalPeople)
                {
                    physicalPeopleDTO.Add(new PhysicalPersonDTO()
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
            return physicalPeopleDTO;
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
                        INN = personInfo.Inn,
                        KPP = personInfo.Kpp,
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

        public static List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsLegalPersonDTO = new List<AnimalCardDTO>();
            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkLegalPerson == legalPersonId)
                    .Select(x => x.FkAnimalCard)
                    .Distinct();

                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id == animalId)
                        .FirstOrDefault();
                    animalsLegalPersonDTO.Add(
                        new AnimalCardDTO()
                        {
                            Id = animal.Id,
                            IsBoy = animal.IsBoy,
                            Name = animal.Name,
                            Photo = animal.Photo,
                            YearOfBirth = animal.YearOfBirth,
                            FkCategory = animal.FkCategory,
                            FkShelter = animal.FkShelter,
                            ChipId = animal.ChipId,
                        });
                }
                return animalsLegalPersonDTO;
            }
        }
    }
}
