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
using PIS_PetRegistry.Services;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.Backend;

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
            var legalPersonModel = DTOModelConverter.ConvertLegalPersonDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);
             var newLegalPersonDTO = DTOModelConverter.ConvertLegalPersonModelToDTO(legalPersonModel);

            return newLegalPersonDTO;
        }

        public static LegalPersonDTO AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertLegalPersonDTOToModel(legalPersonDTO);
            PetOwnersService.AddLegalPerson(legalPersonModel);
            var newLegalPersonDTO = DTOModelConverter.ConvertLegalPersonModelToDTO(legalPersonModel);

            return newLegalPersonDTO;
        }

        public static PhysicalPersonDTO AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertPhysicalPersonDTOToModel(physicalPersonDTO);
            PetOwnersService.AddPhysicalPerson(physicalPersonModel);
            var newPhysicalPersonDTO = DTOModelConverter.ConvertPhysicalPersonModelToDTO(physicalPersonModel);

            return newPhysicalPersonDTO;
        }

        public static PhysicalPersonDTO UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertPhysicalPersonDTOToModel(physicalPersonDTO);
            PetOwnersService.UpdatePhysicalPerson(physicalPersonModel);
            var newPhysicalPersonDTO = DTOModelConverter.ConvertPhysicalPersonModelToDTO(physicalPersonModel);

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
