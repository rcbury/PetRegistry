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
                var personInfo = PetOwnersService.GetPhysicalPersonDetailInfo(physicalPerson.Id,
                    physicalPerson.FkCountry, physicalPerson.FkLocality);
                res.Add(DTOModelConverter.ConvertModelToDTO(physicalPerson, personInfo));
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
            var personInfo = PetOwnersService.GetPhysicalPersonDetailInfo(person.Id,
                person.FkCountry, person.FkLocality);
            return DTOModelConverter.ConvertModelToDTO(person, personInfo);
        }

        public static PhysicalPersonDTO? GetPhysicalPersonById(int personId)
        {
            var person = PetOwnersService.GetPhysicalPersonById(personId);
            if (person == null)
            {
                return null;
            }
            var personInfo = PetOwnersService.GetPhysicalPersonDetailInfo(person.Id,
                person.FkCountry, person.FkLocality);
            return DTOModelConverter.ConvertModelToDTO(person, personInfo);
        }

        public static LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = PetOwnersService.GetLegalPersonByInn(INN);
            if (person == null)
            {
                return null;
            }
            var personInfo = PetOwnersService.GetLegalPersonDetailInfo(person.Id,
                person.FkCountry, person.FkLocality);
            return DTOModelConverter.ConvertModelToDTO(person, personInfo);
        }
        public static LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = PetOwnersService.GetLegalPersonById(personId);
            if (person == null)
            {
                return null;
            }
            var personInfo = PetOwnersService.GetLegalPersonDetailInfo(person.Id,
                person.FkCountry, person.FkLocality);
            return DTOModelConverter.ConvertModelToDTO(person, personInfo);
        }

        public static List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email, 
            string address, string phone, int country, int location)
        {
            var legalPeople = PetOwnersService.GetLegalPeople(inn, kpp, name, email, address, phone, country, location);
            var res = new List<LegalPersonDTO>();
            foreach (var legalPerson in legalPeople)
            {
                var personInfo = PetOwnersService.GetLegalPersonDetailInfo(legalPerson.Id,
                    legalPerson.FkCountry, legalPerson.FkLocality);
                res.Add(DTOModelConverter.ConvertModelToDTO(legalPerson, personInfo));
            }
            return res;
        }
       
        public static LegalPersonDTO UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);
            var personInfo = PetOwnersService.GetLegalPersonDetailInfo(legalPersonDTO.Id,
                legalPersonDTO.FkCountry, legalPersonDTO.FkLocality);
            var newLegalPersonDTO = DTOModelConverter.ConvertModelToDTO(legalPersonModel, personInfo);

            return newLegalPersonDTO;
        }

        public static LegalPersonDTO AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.AddLegalPerson(legalPersonModel);
            var personInfo = PetOwnersService.GetLegalPersonDetailInfo(legalPersonDTO.Id,
                legalPersonDTO.FkCountry, legalPersonDTO.FkLocality);
            var newLegalPersonDTO = DTOModelConverter.ConvertModelToDTO(legalPersonModel, personInfo);

            return newLegalPersonDTO;
        }

        public static PhysicalPersonDTO AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertDTOToModel(physicalPersonDTO);
            PetOwnersService.AddPhysicalPerson(physicalPersonModel);
            var personInfo = PetOwnersService.GetPhysicalPersonDetailInfo(physicalPersonModel.Id,
                physicalPersonModel.FkCountry, physicalPersonModel.FkLocality);
            var newPhysicalPersonDTO = DTOModelConverter.ConvertModelToDTO(physicalPersonModel, personInfo);

            return newPhysicalPersonDTO;
        }

        public static PhysicalPersonDTO UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertDTOToModel(physicalPersonDTO);
            PetOwnersService.UpdatePhysicalPerson(physicalPersonModel);
            var personInfo = PetOwnersService.GetPhysicalPersonDetailInfo(physicalPersonModel.Id,
                physicalPersonModel.FkCountry, physicalPersonModel.FkLocality);
            var newPhysicalPersonDTO = DTOModelConverter.ConvertModelToDTO(physicalPersonModel, personInfo);

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

        public static int CountAnimalsByPhysicalPerson(int physicalPersonId)
        {
            return PetOwnersService.GetPhysicalPersonAnimalCount(physicalPersonId);
        }

        public static int CountAnimalsByLegalPerson(int legalPersonId)
        {
            return PetOwnersService.GetLegalPersonAnimalCount(legalPersonId);
        }
    }
}
