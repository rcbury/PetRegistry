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

        public static List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsByLegalPersonDTO = new List<AnimalCardDTO>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkLegalPerson.Equals(legalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .Distinct();

                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByLegalPersonDTO.Add(
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
            }
            return animalsByLegalPersonDTO;
        }

        public static List<AnimalCardDTO> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsByPhysicalPersonDTO = new List<AnimalCardDTO>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkPhysicalPerson.Equals(physicalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .Distinct();

                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByPhysicalPersonDTO.Add(
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
            }

            return animalsByPhysicalPersonDTO;
        }
       
        public static LegalPersonDTO UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            LegalPerson? legalPersonModel;

            using (var context = new RegistryPetsContext())
            {
                legalPersonModel = context.LegalPeople
                    .Where(x => x.Id.Equals(legalPersonDTO.Id))
                    .FirstOrDefault();

                if (legalPersonModel == null)
                {
                    throw new Exception("trying to change unexisting legal person");
                }

                legalPersonModel.Inn = legalPersonDTO.INN;
                legalPersonModel.Kpp = legalPersonDTO.KPP;
                legalPersonModel.Name= legalPersonDTO.Name;
                legalPersonModel.Address = legalPersonDTO.Address;
                legalPersonModel.Email = legalPersonDTO.Email;
                legalPersonModel.Phone = legalPersonDTO.Phone;
                legalPersonModel.FkCountry= legalPersonDTO.FkCountry;
                legalPersonModel.FkLocality = legalPersonDTO.FkLocality;

                context.SaveChanges();
            }

            var newLegalPersonDTO = new LegalPersonDTO()
            {
                Id = legalPersonModel.Id,
                INN = legalPersonModel.Inn,
                KPP = legalPersonModel.Kpp,
                Name = legalPersonModel.Name,
                Address = legalPersonModel.Address,
                Email = legalPersonModel.Email,
                Phone = legalPersonModel.Phone,
                FkCountry = legalPersonModel.FkCountry,
                FkLocality = legalPersonModel.FkLocality,
            };

            return newLegalPersonDTO;
        }

        public static LegalPersonDTO AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = new LegalPerson()
            {
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            using (var context = new RegistryPetsContext())
            {
                context.LegalPeople.Add(legalPersonModel);
                context.SaveChanges();
            }

            var newLegalPersonDTO = new LegalPersonDTO()
            {
                Id = legalPersonModel.Id,
                INN = legalPersonModel.Inn,
                KPP = legalPersonModel.Kpp,
                Name = legalPersonModel.Name,
                Address = legalPersonModel.Address,
                Email = legalPersonModel.Email,
                Phone = legalPersonModel.Phone,
                FkCountry = legalPersonModel.FkCountry,
                FkLocality = legalPersonModel.FkLocality,
            };

            return newLegalPersonDTO;
        }

        public static PhysicalPersonDTO AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = new PhysicalPerson()
            {
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            using (var context = new RegistryPetsContext())
            {
                context.PhysicalPeople.Add(physicalPersonModel);
                context.SaveChanges();
            }

            var newPhysicalPersonDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            return newPhysicalPersonDTO;
        }

        public static PhysicalPersonDTO UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            PhysicalPerson? physicalPersonModel;

            using (var context = new RegistryPetsContext())
            {
                physicalPersonModel = context.PhysicalPeople
                    .Where(x => x.Id.Equals(physicalPersonDTO.Id))
                    .FirstOrDefault();

                if(physicalPersonModel == null)
                {
                    throw new Exception("trying to change unexisting physical person");
                }

                physicalPersonModel.Name = physicalPersonDTO.Name;
                physicalPersonModel.Address = physicalPersonDTO.Address;
                physicalPersonModel.Email = physicalPersonDTO.Email;
                physicalPersonModel.Phone = physicalPersonDTO.Phone;
                physicalPersonModel.FkCountry = physicalPersonDTO.FkCountry;
                physicalPersonModel.FkLocality = physicalPersonDTO.FkLocality;

                context.SaveChanges();
            }

            var newPhysicalPersonDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonModel.Name,
                Address = physicalPersonModel.Address,
                Email = physicalPersonModel.Email,
                Phone = physicalPersonModel.Phone,
                FkCountry = physicalPersonModel.FkCountry,
                FkLocality= physicalPersonModel.FkLocality,
            };

            return newPhysicalPersonDTO;
        }

        
    }
}
