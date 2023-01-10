using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    public class PetOwnersService
    {
        public static List<PhysicalPerson> GetPhysicalPeople(string phone, string name, string address, string email, int country, int location) 
        {
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
                return physicalPeople;
            }
        }

        public static List<LegalPerson> GetLegalPeople(string inn, string kpp, string name, string email, 
            string address, string phone, int country, int location)
        {
            using (var context = new RegistryPetsContext())
            {
                var legalPeople = context.LegalPeople.ToList();
                if (inn != null && inn != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(inn)).ToList();
                }
                if (kpp != null && kpp != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(kpp)).ToList();
                }
                if (name != null && name != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(name)).ToList();
                }
                if (email != null && email != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(email)).ToList();
                }
                if (address != null && address != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(address)).ToList();
                }
                if (phone != null && phone != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(phone)).ToList();
                }
                if (country != 0)
                {
                    legalPeople = legalPeople.Where(person => person.FkCountry == country).ToList();
                }
                if (location != 0)
                {
                    legalPeople = legalPeople.Where(person => person.FkLocality == location).ToList();
                }
                return legalPeople;
            }
        }

        public static LegalPerson? GetLegalPersonById(int? personId)
        {
            if (personId == null) 
            {
                return null;
            }
            using (var context = new RegistryPetsContext()) 
            {
                return context.LegalPeople
                    .Where(person => person.Id == personId)
                    .Include(person => person.FkLocalityNavigation)
                    .FirstOrDefault();
            }
        }

        public static PhysicalPerson? GetPhysicalPersonById(int personId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.PhysicalPeople
                    .Where(person => person.Id == personId)
                    .Include(person => person.FkLocalityNavigation)
                    .FirstOrDefault();
            }
        }

        public static PhysicalPerson? GetPhysicalPersonByPhone(string phone)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.PhysicalPeople.Where(person => person.Phone == phone).FirstOrDefault();
            }
        }
        public static LegalPerson? GetLegalPersonByInn(string INN)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.LegalPeople.Where(person => person.Inn == INN).FirstOrDefault();
            }
        }
        public static LegalPerson AddLegalPerson(LegalPerson legalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.LegalPeople.Add(legalPersonModel);
                context.SaveChanges();
            }

            return legalPersonModel;
        }

        public static PhysicalPerson AddPhysicalPerson(PhysicalPerson physicalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.PhysicalPeople.Add(physicalPersonModel);
                context.SaveChanges();
            }

            return physicalPersonModel;
        }

        public static LegalPerson UpdateLegalPerson(LegalPerson legalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.Update(legalPersonModel);
                context.SaveChanges();
            }

            return legalPersonModel;
        }

        public static PhysicalPerson UpdatePhysicalPerson(PhysicalPerson physicalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.Update(physicalPersonModel);
                context.SaveChanges();
            }

            return physicalPersonModel;
        }

        public static PersonInfo GetPhysicalPersonDetailInfo(int personId, int countryId, int locationId) 
        {
            var physicalPerson = GetPhysicalPersonById(personId);

            //var animalCount = GetPhysicalPersonAnimalCount(personId);
            var animalCount = physicalPerson.GetAnimalCount();

            var catCount = physicalPerson.GetCatCount();
            var dogCount = physicalPerson.GetDogCount();
            var countryName = CountryService.GetCountryNameById(countryId);
            var locationName = LocationService.GetLocationNameById(locationId);
            var personDetailInfoDTO = new PersonInfo()
            {
                AnimalCount = animalCount,
                CatCount = catCount,
                DogCount = dogCount,
                CountryName = countryName,
                LocationName = locationName
            };
            return personDetailInfoDTO;
        }

        public static PersonInfo GetLegalPersonDetailInfo(int personId, int countryId, int locationId)
        {
            var legalPerson = GetLegalPersonById(personId);


            var animalCount = legalPerson.GetAnimalCount();
            var catCount = legalPerson.GetCatCount();
            var dogCount = legalPerson.GetDogCount();
            var countryName = CountryService.GetCountryNameById(countryId);
            var locationName = LocationService.GetLocationNameById(locationId);
            var personDetailInfoDTO = new PersonInfo()
            {
                AnimalCount = animalCount,
                CatCount = catCount,
                DogCount = dogCount,
                CountryName = countryName,
                LocationName = locationName
            };
            return personDetailInfoDTO;
        }

    }
}
