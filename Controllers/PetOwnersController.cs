using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
            var physicalPeople = PetOwnersService.GetPhysicalPeople(phone,name,address,email,country,location);
            var res = new List<PhysicalPersonDTO>();
            foreach (var physicalPerson in physicalPeople) 
            {
                var animalCount = PetOwnersService.GetPhysicalPersonAnimalCount(physicalPerson.Id);
                var catCount = PetOwnersService.GetPhysicalPersonCatCount(physicalPerson.Id);
                var dogCount = PetOwnersService.GetPhysicalPersonDogCount(physicalPerson.Id);
                res.Add(new PhysicalPersonDTO()
                {
                    Id = physicalPerson.Id,
                    Name = physicalPerson.Name,
                    Phone = physicalPerson.Phone,
                    Address = physicalPerson.Address,
                    Email = physicalPerson.Email,
                    FkLocality = physicalPerson.FkLocality,
                    FkCountry = physicalPerson.FkCountry,
                    AnimalCount = animalCount,
                    CatCount = catCount,
                    DogCount = dogCount,
                    CountryName = CountryService.GetCountryNameById(physicalPerson.FkCountry),
                    LocationName = LocationService.GetLocationNameById(physicalPerson.FkLocality)
                });
            }
            return res;
        }

        public static PhysicalPersonDTO? GetPhysicalPersonByPhone(string phone)
        {
            var person = PetOwnersService.GetPhysicalPersonByPhone(phone);
            if (person == null)
            {
                return null;
            }
            return new PhysicalPersonDTO()
            {
                Id = person.Id,
                Phone = phone,
                Name = person.Name,
                Address = person.Address,
                Email = person.Email,
                FkLocality = person.FkLocality
            };
        }

        public static PhysicalPersonDTO? GetPhysicalPersonById(int personId)
        {
            var person = PetOwnersService.GetPhysicalPersonById(personId);
            if (person == null)
            {
                return null;
            }
            return new PhysicalPersonDTO()
            {
                Id = person.Id,
                Phone = person.Phone,
                Name = person.Name,
                Address = person.Address,
                Email = person.Email,
                FkLocality = person.FkLocality
            };
        }

        public static LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = PetOwnersService.GetLegalPersonByInn(INN);
            if (person == null)
            {
                return null;
            }
            return new LegalPersonDTO()
            {
                Id = person.Id,
                INN = INN,
                KPP = person.Kpp,
                Name = person.Name,
                Address = person.Address,
                Email = person.Email,
                Phone = person.Phone,
                FkLocality = person.FkLocality
            };
        }
        public static LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = PetOwnersService.GetLegalPersonById(personId);
            if (person == null)
            {
                return null;
            }
            else 
            {
                return new LegalPersonDTO()
                {
                    Id = person.Id,
                    INN = person.Inn,
                    KPP = person.Kpp,
                    Name = person.Name,
                    Address = person.Address,
                    Email = person.Email,
                    Phone = person.Phone,
                    FkLocality = person.FkLocality
                };
            }
        }

        public static List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email, 
            string address, string phone, int country, int location)
        {
            var legalPeople = PetOwnersService.GetLegalPeople(inn, kpp, name, email, address, phone, country, location);
            var res = new List<LegalPersonDTO>();
            foreach (var personInfo in legalPeople)
            {
                var animalCount = PetOwnersService.GetLegalPersonAnimalCount(personInfo.Id);
                var catCount = PetOwnersService.GetLegalPersonCatCount(personInfo.Id);
                var dogCount = PetOwnersService.GetLegalPersonDogCount(personInfo.Id);
                res.Add(new LegalPersonDTO()
                {
                    Id = personInfo.Id,
                    INN = personInfo.Inn,
                    KPP = personInfo.Kpp,
                    Name = personInfo.Name,
                    Phone = personInfo.Phone,
                    Address = personInfo.Address,
                    Email = personInfo.Email,
                    FkLocality = personInfo.FkLocality,
                    FkCountry = personInfo.FkCountry,
                    AnimalCount = animalCount,
                    CatCount = catCount,
                    DogCount = dogCount,
                    CountryName = CountryService.GetCountryNameById(personInfo.FkCountry),
                    LocationName = LocationService.GetLocationNameById(personInfo.FkLocality)
                });
            }
            return res;
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

        public static void ExportPhysicalPeopleToExcel(string path, List<PhysicalPersonDTO> physicalPeopleDTO)
        {
            Exporter.ExportPhysicalPeopleToExcel(path, physicalPeopleDTO);
        }

        public static void ExportLegalPeopleToExcel(string path, List<LegalPersonDTO> legalPeopleDTO) 
        {
            Exporter.ExportLegalPeopleToExcel(path, legalPeopleDTO);
        }
    }
}
